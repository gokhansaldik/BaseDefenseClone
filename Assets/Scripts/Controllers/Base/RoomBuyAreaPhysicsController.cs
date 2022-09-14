using Managers;
using UnityEngine;

namespace Controllers
{
    public class RoomBuyAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                roomManager.BuyAreaEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                roomManager.BuyAreaExit();
            }
        }
    }
}