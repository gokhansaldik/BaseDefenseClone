using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretShootRangeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager manager;
        [SerializeField] private List<Transform> targetList;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform turretRotatableObj;
        [SerializeField] private GameObject currentBullet;
        [SerializeField] private Transform sight;

        #region Private Variables

        private int _firedBulletIndex;
        private ParticleSystem _turretParticle;

        #endregion

        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _turretParticle = GetComponent<ParticleSystem>();
        }


        private GameObject GetBullet()
        {
            return Resources.Load<GameObject>("Bullets/TurretBullet");
        }


        private void Start()
        {
            StartCoroutine(GunFire());
            currentBullet = GetBullet();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (targetList.Contains(other.transform)) return;

                targetList.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy")) targetList.Remove(other.transform);
        }

        private void Update()
        {
            if (manager.HasOwner && targetList.Count > 0 && manager.AmmoBoxList.Count > 0)
            {
                currentTarget = targetList[0];
                if (currentTarget.Equals(null))
                {
                    targetList.RemoveAt(0);
                    return;
                }

                var lookAtPos = new Vector3(currentTarget.position.x, 0, currentTarget.position.z);
                turretRotatableObj.LookAt(lookAtPos);
            }

            else if (targetList.Count == 0)
            {
            }
        }

        private IEnumerator GunFire()
        {
            if ((manager.HasOwner || manager.IsPlayerUsing) && targetList.Count > 0 && manager.AmmoBoxList.Count > 0) //
            {
                UseAmmo();
                Instantiate(currentBullet, sight.transform.position, sight.rotation);
                _turretParticle.Play();
            }

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(GunFire());
        }

        private void UseAmmo()
        {
            _firedBulletIndex++;
            if (_firedBulletIndex.Equals(3))
            {
                Destroy(manager.AmmoBoxList[manager.AmmoBoxList.Count - 1].gameObject);
                manager.AmmoBoxList.RemoveAt(manager.AmmoBoxList.Count - 1);
                _firedBulletIndex = 0;
            }
        }

        public void OnRemoveFromTargetList(Transform deadEnemy)
        {
            if (targetList.Contains(deadEnemy)) targetList.Remove(deadEnemy);
        }
    }
}