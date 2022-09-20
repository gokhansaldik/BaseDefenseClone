using Managers;
using UnityEngine;

namespace Controllers.Mine
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MineManager mineManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                mineManager.StartCollectDiamond(other.gameObject);
            }
        }
    }
}