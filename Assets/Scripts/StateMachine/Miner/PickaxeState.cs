using Enums;
using Interface;
using Managers;
using UnityEngine;


namespace StateMachine.Miner
{
    public class PickaxeState : IMinerStateMachine
    {
        #region Self Variables
        
        #region Private Variables

        private MinerManager _minerManager;

        #endregion
        #endregion

        public PickaxeState(MinerManager minerManager)
        {
            _minerManager = minerManager;
        }

        #region States
        public void EnterState()
        {
            DigDiamond();
            _minerManager.SetTriggerAnim(MinerAnimType.Dig);
        }
        public void UpdateState()
        {
        }
        public void CollisionState(Collider other)
        {
        }
        private void DigDiamond()
        {
            _minerManager.StartCoroutine(_minerManager.DigDiamond());
        }
        public void SwitchState()
        {
            _minerManager.SwitchState(MinerStatesType.GoStacking);
        }
        #endregion
    }
}