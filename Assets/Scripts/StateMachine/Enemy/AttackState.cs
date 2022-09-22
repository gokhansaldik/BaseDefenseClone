using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class AttackState : IState
    {
        private IEnemyController _enemyController;
        public AttackState(IEnemyController enemyController)
        {
            _enemyController = enemyController;
        }
        public void OnEnter()
        {
            Debug.Log($"{nameof(AttackState)}{nameof(OnEnter)}");
        }
        public void OnExit()
        {
            _enemyController.Animation.AttackAnim(false);
        }
        public void Tick()
        {
            _enemyController.Animation.AttackAnim(true);
        }

       

        
    }
}
