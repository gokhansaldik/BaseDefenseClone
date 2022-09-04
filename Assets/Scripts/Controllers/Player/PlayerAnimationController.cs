using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimatorController;

        #endregion

        #endregion

        // public void PlayAnim(float Value)
        // {
        //     animatorController.SetFloat("Speed", Value);
        // }
        // public void ChangeCollectableAnimation(PlayerAnimationStates playerAnimationStates)
        // {
        //     switch (playerAnimationStates)
        //     {
        //         case PlayerAnimationStates.Idle:
        //             animatorController.Play(playerAnimationStates.ToString());
        //             break;
        //         case PlayerAnimationStates.Run:
        //             animatorController.Play(playerAnimationStates.ToString());
        //             break;
        //     }
        // }
        public void ChangeAnimationState(PlayerAnimationStates type)
        {
            playerAnimatorController.Play(type.ToString());
        }
    }
}