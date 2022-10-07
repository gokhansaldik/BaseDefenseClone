using Managers;
using UnityEngine;

namespace Controllers.Mine
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int MinerCount;

        #endregion

        #region Serialized Variables

        [SerializeField] private MineManager mineManager;
        [SerializeField] private StackManager stackManager;

        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected") && MinerCount < 5)
            {
                MinerCount++;
                mineManager.minerCountText.text = MinerCount.ToString() + "/" + "5";
            }
        }
    }
}