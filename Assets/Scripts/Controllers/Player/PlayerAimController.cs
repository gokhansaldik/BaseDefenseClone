using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private List<Transform> targetList;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform targetGameObject;
        [SerializeField] private GameObject currentBullet;
        [SerializeField] private Transform sight;
        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion

        #endregion
        
        private void Start()
        {
            StartCoroutine(Shoot());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targetList.Add(other.transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targetList.Remove(other.transform);
            }
        }
        private void LateUpdate()
        {
            if (targetList.Count > 0)
            {
                currentTarget = targetList[0];
                playerMovementController.Target = currentTarget;
                targetGameObject.position = currentTarget.position;
            }
            else if (targetList.Count == 0)
            {
                targetGameObject.localPosition = new Vector3(0, 7.5f, 10f);
                playerMovementController.Target = null;
            }
        }
        private IEnumerator Shoot()
        {
            if (targetList.Count > 0)
            {
                Instantiate(currentBullet, sight.transform.position, sight.rotation);
                AimAt(targetList[0]);
            }
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Shoot());
        }
        public void OnRemoveFromTargetList(Transform deadEnemy)
        {
            if (targetList.Contains(deadEnemy))
            {
                targetList.Remove(deadEnemy);
            }
        }
        public void AimAt(Transform target)
        {
            transform.LookAt(new Vector3(target.position.x, target.position.y + 0.3f, target.position.z),
                transform.up);
        }
    }
}