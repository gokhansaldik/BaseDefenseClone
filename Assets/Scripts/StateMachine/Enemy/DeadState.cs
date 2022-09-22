using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class DeadState : IState
    {
        public void OnEnter()
        {
            Debug.Log($"{nameof(DeadState)}{nameof(OnEnter)}");
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(DeadState)}{nameof(OnExit)}");
        }
        public void Tick()
        {
            Debug.Log($"{nameof(DeadState)}");
        }

       
    }
}
