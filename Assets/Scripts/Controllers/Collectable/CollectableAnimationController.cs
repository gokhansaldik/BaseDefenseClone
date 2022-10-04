using Enums;
using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator collectableAnimatorController;

        #endregion

        #endregion

        public void PlayAnim(CollectableAnimationStates animationStates)
        {
            collectableAnimatorController.SetTrigger(animationStates.ToString());
        }
    }
}