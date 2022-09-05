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
        
        public void ChangeAnimationState(PlayerAnimationStates type)
        {
            playerAnimatorController.Play(type.ToString());
        }
    }
}