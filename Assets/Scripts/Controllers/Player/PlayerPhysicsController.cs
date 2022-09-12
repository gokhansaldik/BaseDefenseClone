using Managers;
using Signals;
using UnityEngine;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectablePlayer"))
            {
                StackSignals.Instance.onAddInStack?.Invoke(other.gameObject.transform.parent.gameObject);
                other.gameObject.tag = "Collected";
            }

            else if (other.CompareTag("CollectableMoney"))
            {
                playerManager.AddStack(other.gameObject);
                other.gameObject.tag = "CollectedMoney";
            }
           
        }

        private void OnTriggerStay(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }
    }
}