using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class ChaseState : IState
    {
        private float _speed = 10f;
        private IEntityController _entityController;
        private Transform _target;
        public ChaseState(IEntityController entityController, Transform target)
        {
            _entityController = entityController;
            _target = target;
        }
        public void OnEnter()
        {
            Debug.Log($"{nameof(ChaseState)}{nameof(OnEnter)}");
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(ChaseState)}{nameof(OnExit)}");
        }
        public void Tick()
        {
            _entityController.Mover.MoveAction(_target.position,_speed);
        }

      
    }
}
