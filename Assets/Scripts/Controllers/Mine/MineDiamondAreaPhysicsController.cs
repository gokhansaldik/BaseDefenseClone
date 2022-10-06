using Managers;
using UnityEngine;

namespace Controllers.Mine
{
    public class MineDiamondAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MineManager mineManager;
      
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
               
                mineManager.StartCollectDiamond(other.gameObject);
              
            }
        }
    }
}