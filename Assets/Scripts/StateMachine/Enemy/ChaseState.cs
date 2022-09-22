using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class ChaseState : IState
    {
        private float _speed = 10f;
        private IEnemyController _enemyController;
        private Transform _target;
        public ChaseState(IEnemyController enemyController, Transform target)
        {
            _enemyController = enemyController;
            _target = target;
        }
        public void OnEnter()
        {
            _enemyController.Animation.MoveAnimation(0f);
        }
        public void OnExit()
        {
            _enemyController.Animation.MoveAnimation(0.5f);
        }
        public void Tick()
        {
            _enemyController.Mover.MoveAction(_target.position,_speed);
        }

      
    }
}
