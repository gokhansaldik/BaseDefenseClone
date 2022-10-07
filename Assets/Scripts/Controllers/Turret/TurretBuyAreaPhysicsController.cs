using Managers;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretBuyAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] private TurretAreaManager manager;

        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.BuyAreaEnter();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.BuyAreaExit();
            }
        }
    }
}