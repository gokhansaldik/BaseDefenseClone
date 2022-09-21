using Abstract;
using Enums;
using Interface;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine.Miner
{
    public class WaitState : IMinerStateMachine
    {
        #region Self Variables

        

        #region Private Variables

        private MinerManager _minerManager;
        private NavMeshAgent _navmeshAgent;

        #endregion

        #endregion

        public WaitState(MinerManager minerManager, ref NavMeshAgent navmeshAgent)
        {
            _minerManager = minerManager;
            _navmeshAgent = navmeshAgent;
        }

        public void EnterState()
        {
            _minerManager.SetTriggerAnim(MinerAnimType.Idle);
        }

        public void UpdateState()
        {
        }

        public void CollisionState(Collider other)
        {
        }

        public void SwitchState()
        {
            _minerManager.SwitchState(MinerStatesType.GoStacking);
        }
    }
}