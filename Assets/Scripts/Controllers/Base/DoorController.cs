using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DoorController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject baseDoor;

        #endregion

        private PlayerManager _playerManager;

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OpenDoor();
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CloseDoor();
            }
        }

        private void OpenDoor()
        {
            baseDoor.transform.DOLocalRotate(new Vector3(0, 0, 90f), 1f);
        }

        private void CloseDoor()
        {
            baseDoor.transform.DOLocalRotate(Vector3.zero, 1f);
        }

       
    }
}