using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectablePlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject collectablePlayer;
        [SerializeField] private GameObject miner;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineArea"))
            {
                collectablePlayer.SetActive(false);
                miner.SetActive(true);
            }
        }
    }
}