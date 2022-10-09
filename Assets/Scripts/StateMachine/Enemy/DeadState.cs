using Controllers.Enemy;
using Enums;
using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class DeadState : IState
    {
        private IEnemyController _enemyController;
        private float _maxTime = 5;
        private float _currentTime = 0;
       

        public DeadState(IEnemyController enemyController)
        {
            _enemyController = enemyController;
            _enemyController.Dead.DeadAction();
          
        }
        #region States

        public void OnEnter()
        {
        }
        public void OnExit()
        {
        }
        public void Tick()
        {
        }
        #endregion
    }
}