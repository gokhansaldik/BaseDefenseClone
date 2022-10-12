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

        [SerializeField] private HealthManager healthManager;

        #endregion

        #region Private Variables

        private ParticleSystem enemyBlood;

        #endregion

        #endregion

        private void Start()
        {
            enemyBlood = GetComponent<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                EnemySignals.Instance.onPlayerDamage();
            else if (other.CompareTag("PistolBullet"))
                if (healthManager.CurrentHealth >= 10)
                {
                    healthManager.CurrentHealth -= 10;
                    var parent = gameObject.transform.parent;
                    parent.DOShakePosition(0.15f, new Vector3(0, 0, 0.2f));
                    healthManager.EnemyAnim();
                    enemyBlood.Play();
                }
        }
    }
}