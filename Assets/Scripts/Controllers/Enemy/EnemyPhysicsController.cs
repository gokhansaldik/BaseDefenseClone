using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Material enemyMaterial;
        [SerializeField] private HealthManager healthManager;
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnemySignals.Instance.onPlayerDamage();
            }
            else if (other.CompareTag("PistolBullet"))
            {
                if (healthManager.CurrentHealth >=10)
                {
                    healthManager.CurrentHealth -= 10;
                    var parent = gameObject.transform.parent;
                    parent.DOShakePosition(0.15f, new Vector3(0, 0, 0.2f), 10, 90);
                    //parent.DOPunchPosition(new Vector3(0, 0, 0.5f), 2f, 1, 2f, true);
                    healthManager.EnemyAnim();
                }

            }
        }
    }
}