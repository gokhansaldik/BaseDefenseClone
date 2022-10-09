using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class ChaseState : IState
    {
        #region Self Variables

        #region Private Variables

        private float _speed = 10f;
        private IEnemyController _enemyController;
        private Transform _target;

        #endregion
        #endregion

        public ChaseState(IEnemyController enemyController, Transform target)
        {
            _enemyController = enemyController;
            _target = target;
        }
        #region States
        public void OnEnter()
        {
            _enemyController.Animation.MoveAnimation(0f);
        }

        public void OnExit()
        {
            _enemyController.Animation.MoveAnimation(0.5f);
            _enemyController.Mover.MoveAction(_enemyController.transform.position,0f);
        }

        public void Tick()
        {
            _enemyController.Mover.MoveAction(_target.position, _speed);
        }
        #endregion
    }
}