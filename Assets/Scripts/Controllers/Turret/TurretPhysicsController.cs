using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager manager;
        
        #endregion

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")&& !manager.HasOwner)
            {
                PlayerSignals.Instance.onPlayerUseTurret?.Invoke(true);
                manager.PlayerUseTurret(other.transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !manager.HasOwner)
            {
                manager.PlayerLeaveTurret(other.transform);
                //PlayerSignals.Instance.onPlayerUseTurret?.Invoke(false);
                return;
            }
        }

       
    }
}