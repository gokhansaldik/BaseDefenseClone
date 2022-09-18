using Abstract;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachine
{
    public class GoMineState : IState
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

        public GoMineState( MinerManager manager,ref NavMeshAgent agent)
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
            _manager.SetAnim(MinerAnimType.Run);
            _manager.SetAnimLayer(AnimLayerType.UpperBody,0);
            _manager.Diamond.SetActive(false);

        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
            if (other.CompareTag("Mine"))
            {
                SwitchState();
            }
           
        }
        
        public void SwitchState()
        {
            //_manager.SwitchState(_manager.Dig);
        }
    }
}