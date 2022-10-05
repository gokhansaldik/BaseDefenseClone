using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private Material enemyMaterial;
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
                   // parent.DOMove(new Vector3(0,0,parent.position.z-0.01f),1f); //TODO: Geri tepme ama bug var
                   parent.DOShakePosition(0.15f, new Vector3(0, 0, 0.2f), 7, 20);
                   
                   // if (healthManager.CurrentHealth <=0)
                   // {
                   //     enemyMaterial.DOColor(Color.black, 0.2f);
                   // }
                   healthManager.EnemyAnim();
                }
                
            }
        }
    }
}