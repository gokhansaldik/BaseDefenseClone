using Enums;
using Managers;
using Signals;
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
        private readonly StackManager _manager;

        #endregion

        #region Private Variables

        private int _timer;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")&& !isTaken)
            {
                //var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                //  StackSignals.Instance.onAddInStack?.Invoke(gameObject);
                //  if (!otherPhysic.isTaken)
                //  {
                //      otherPhysic.isTaken = true;
                // }
                // StackSignals.Instance.onAddInStack?.Invoke(other.gameObject);
                // Debug.Log("Collectable Physics");
                // var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Collectable);
                // _manager.AddInStack(obj);
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