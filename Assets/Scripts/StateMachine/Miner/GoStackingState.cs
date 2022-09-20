using Abstract;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachine.Miner
{
    public class GoStackingState : IMinerStateMachine
    {
        #region Self Variables
        
        #region Private Variables

        private MinerManager _minerManager;
        private NavMeshAgent _navmeshAgent;

        #endregion

        #endregion

        public GoStackingState(MinerManager minerManager, ref NavMeshAgent navmeshAgent)
        {
            _minerManager = minerManager;
            _navmeshAgent = navmeshAgent;
        }

        public void EnterState()
        {
            _navmeshAgent.SetDestination(_minerManager.Stack.transform.position);
            _minerManager.SetTriggerAnim(MinerAnimType.Run);
            _minerManager.SetAnimLayer(AnimationLayerType.UpperBody, 1);
        }

        public void UpdateState()
        {
        }

        public void CollisionState(Collider other)
        {
            if (other.CompareTag("MineWareHouse"))
            {
                PushDiamondOnStack();
            }
        }

        private void PushDiamondOnStack()
        {
            IdleGameSignals.Instance.onAddDiamondStack?.Invoke(_minerManager.transform.gameObject);
            SwitchState();
        }


        public void SwitchState()
        {
            _minerManager.SwitchState(MinerStatesType.GoDigging);
        }
    }
}