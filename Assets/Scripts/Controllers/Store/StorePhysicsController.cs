using Enums;
using Signals;
using UnityEngine;

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
                UISignals.Instance.onOpenStorePanel?.Invoke(releatedPanel);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
            }
        }
    }
}