using Managers;
using UnityEngine;

namespace Controllers.Miner
{
    public class MinerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MinerManager minerManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            minerManager.CurrentState.CollisionState(other);
        }
    }
}