using Abstract;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class WaitState:IState
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private MinerManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public WaitState( MinerManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void UpdateIState()
        {
            
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void EnterState()
        {
            _agent.SetDestination(_manager.Target.transform.position);
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
        }

        public void SwitchState()
        {
        }
    }
}