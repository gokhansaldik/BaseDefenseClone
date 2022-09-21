using Abstract;
using Enums;
using Interface;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine.Miner
{
    public class GoDiggingState : IMinerStateMachine
    {
        #region Self Variables

        #region Private Variables

        private MinerManager _minerManager;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public GoDiggingState(MinerManager minerManager, ref NavMeshAgent navMeshAgent)
        {
            _minerManager = minerManager;
            _navMeshAgent = navMeshAgent;
        }

        public void EnterState()
        {
            _navMeshAgent.SetDestination(_minerManager.Target.transform.position);
            _minerManager.SetTriggerAnim(MinerAnimType.Run);
            _minerManager.SetAnimLayer(AnimationLayerType.UpperBody, 0);
        }

        public void UpdateState()
        {
        }

        public void CollisionState(Collider other)
        {
            if (other.CompareTag("Mine"))
            {
                SwitchState();
            }
        }

        public void SwitchState()
        {
            _minerManager.SwitchState(MinerStatesType.Pickaxe);
        }
    }
}