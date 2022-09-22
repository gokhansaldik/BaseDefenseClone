using Interface;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class AttackState : IState
    {
        public void OnEnter()
        {
            Debug.Log($"{nameof(AttackState)}{nameof(OnEnter)}");
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(AttackState)}{nameof(OnExit)}");
        }
        public void Tick()
        {
            Debug.Log($"{nameof(AttackState)}");
        }

       

        
    }
}
