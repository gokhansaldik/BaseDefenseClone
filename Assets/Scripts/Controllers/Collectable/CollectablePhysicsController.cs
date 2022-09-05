using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isTaken;

        #endregion

        #region Serializable Variables

        [SerializeField] private CollectableManager collectablemanager;

        #endregion

        #region Private Variables

        private int _timer;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && !isTaken)
            {
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                }
            }


            // if (other.CompareTag("Gate"))
            // {
            //     // var otherColorType = other.GetComponentInParent<GateManager>().ColorType;
            //     // other.GetComponent<Collider>().enabled = false;
            //     // StackSignals.Instance.onChangeCollectableColor?.Invoke(otherColorType);
            // }
        }


        private void OnTriggerExit(Collider other)
        {
        }
    }
}