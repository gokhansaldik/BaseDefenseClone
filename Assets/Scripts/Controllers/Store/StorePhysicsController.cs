using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers.Store
{
    public class StorePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private UIPanels releatedPanel;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //UISignals.Instance.onInitializeGunLevels?.Invoke(UISignals.Instance.onGetGunLevels());
                UISignals.Instance.onOpenStorePanel?.Invoke(releatedPanel);
                return;
            }
    
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //UISignals.Instance.onInitializeGunLevels?.Invoke(UISignals.Instance.onGetGunLevels());
                UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
                return;
            }

        }
    }
}
