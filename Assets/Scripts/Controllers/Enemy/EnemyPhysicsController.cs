using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;
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
                }
                
            }
        }
    }
}