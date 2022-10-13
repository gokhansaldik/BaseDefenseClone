using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAimController : MonoBehaviour
    {
        #region Self Variables

        public List<Transform> targetList;

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform targetGameObject;
        [SerializeField] private GameObject currentBullet;
        [SerializeField] private Transform sight;
        [SerializeField] private PlayerMovementController playerMovementController;
        private ParticleSystem _gunParticle;

        #endregion

        #endregion

        private void Start()
        {
            StartCoroutine(Shoot());
            _gunParticle = GetComponent<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")) targetList.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy")) targetList.Remove(other.transform);
        }

        private void FixedUpdate()
        {
            if (targetList.Count > 0)
            {
                currentTarget = targetList[0];
                playerMovementController.Target = currentTarget;
                targetGameObject.position = currentTarget.position;
            }

            else
            {
                playerMovementController.Target = null;
            }
        }

        private IEnumerator Shoot()
        {
            if (manager.PlayerDead || manager.InBase)
            {
                //do nothing
            }
            else if (targetList.Count > 0 && manager.GunActive)
            {
                Instantiate(currentBullet, sight.transform.position, sight.rotation);
                AimAt(targetList[0]);
                _gunParticle.Play();
            }
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(Shoot());
        }

        public void OnRemoveFromTargetList(Transform deadEnemy)
        {
            if (targetList.Contains(deadEnemy)) targetList.Remove(deadEnemy);
        }

        public void AimAt(Transform target)
        {
            transform.LookAt(new Vector3(target.position.x, target.position.y + 0.3f, target.position.z), transform.up);
        }
    }
}