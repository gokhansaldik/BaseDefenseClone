using Enums;
using UnityEngine;

namespace Controllers.Miner
{
    public class MinerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion

        public void SetAnim(MinerAnimType animType)
        {
            animator.SetTrigger(animType.ToString());
        }

        public void SetLayer(AnimationLayerType type, float weight)
        {
            animator.SetLayerWeight((int)type, weight);
        }
    }
}