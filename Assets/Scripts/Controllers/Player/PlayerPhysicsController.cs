using Managers;
using Signals;
using UnityEngine;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool isTaken;
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
             
                StackSignals.Instance.onAddInStack?.Invoke(other.gameObject);
                Debug.Log("çalıştı");
        
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