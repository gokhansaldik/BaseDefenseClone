using Interface;
using UnityEngine;

namespace Class
{
    public class CharacterAnimation
    {
        #region Self Variables

        #region Private Variables

        private readonly Animator _animator;

        #endregion

        #endregion

        public CharacterAnimation(IEnemyController enemy)
        {
            _animator = enemy.transform.GetComponentInChildren<Animator>();
        }

        public void MoveAnimation(float moveSpeed)
        {
            if (_animator.GetFloat("moveSpeed") == moveSpeed) return;
            _animator.SetFloat("moveSpeed", moveSpeed, 0.1f, Time.deltaTime);
        }

        public void AttackAnim(bool canAttack)
        {
            _animator.SetBool("isAttack", canAttack);
        }
    }
}