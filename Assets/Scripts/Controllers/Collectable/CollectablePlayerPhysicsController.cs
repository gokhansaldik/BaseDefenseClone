using Controllers.Mine;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectablePlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject collectablePlayer;
        [SerializeField] private GameObject miner;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private MinePhysicsController minePhysicsController;
        [SerializeField] private CollectableManager collectableManager;
        #endregion
        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineArea")&& minePhysicsController.MinerCount <5)
            {
                collectablePlayer.SetActive(false);
                miner.SetActive(true);
                StackSignals.Instance.onCollectablePlayerMiner.Invoke();
            }
            if (other.CompareTag("Player") && collectableManager.IsTaken == false)
            {
                collectableManager.SetAnimState(CollectableAnimationStates.Taken);
                collectableManager.IsTaken = true;
            }
        }
    }
}