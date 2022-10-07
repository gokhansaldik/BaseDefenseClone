using DG.Tweening;
using UnityEngine;

namespace Controllers.Base
{
    public class DoorMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject baseDoor;
        
        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
            {
                OpenDoDoor();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
            {
                CloseDoDoor();
                if (other.CompareTag("MoneyWorker"))
                {
                  
                }
                
            }
        }
        private void OpenDoDoor()
        {
            baseDoor.transform.DOLocalRotate(new Vector3(0, 0, 90f), 2f);
        }

        private void CloseDoDoor()
        {
            baseDoor.transform.DOLocalRotate(Vector3.zero, 2f);
        }
    }
}