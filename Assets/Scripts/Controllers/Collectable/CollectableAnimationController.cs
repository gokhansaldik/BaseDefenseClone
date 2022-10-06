using Enums;
using Signals;
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
        public void SetAnimState(CollectableAnimationStates animState)
        {
            collectableAnimatorController.SetTrigger(animState.ToString());
        }

        public void SetSpeedVariable(float speed)
        {
            collectableAnimatorController.SetFloat("Speed", speed);
           // StackSignals.Instance.onCollectablePlayerTaken.Invoke();
        }
    }
}