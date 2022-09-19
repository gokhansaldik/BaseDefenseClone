using Abstract;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachine.Miner
{
    public class GoStackState : MinerStateMachine
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

        public GoStackState(MinerManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            _agent.SetDestination(_manager.Stack.transform.position);
            _manager.SetTriggerAnim(MinerAnimType.Run);
            _manager.SetAnimLayer(AnimLayerType.UpperBody,1);
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
            if (other.CompareTag("MineWareHouse"))
            {
                PushDiamondOnStack();
            }
        }

        private void PushDiamondOnStack()
        {
            IdleGameSignals.Instance.onAddDiamondStack?.Invoke(_manager.transform.gameObject);
            SwitchState();
        }


        public void SwitchState()
        {
            _manager.SwitchState(MinerStatesType.GoMine);
        }
    }
}