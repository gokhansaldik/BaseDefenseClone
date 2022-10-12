using Enums;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator enemyAnimationController;

        #endregion

        #endregion

        public void Playanim(EnemyAnimationStates animationStates)
        {
            enemyAnimationController.SetTrigger(animationStates.ToString());
        }
    }
}