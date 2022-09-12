using Enums;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator collectableAnimatorController;

        #endregion

        #endregion


        public void Playanim(CollectableAnimationStates animationStates)
        {
            collectableAnimatorController.SetTrigger(animationStates.ToString());
        }
    }
}
