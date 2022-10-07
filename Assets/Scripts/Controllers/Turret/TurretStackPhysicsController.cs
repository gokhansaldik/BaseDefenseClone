
using DG.Tweening;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretStackPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BulletBox"))
            {
             other.transform.DOLocalMove(new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
            }
        }
    }
}
