using System.Collections;
using System.Threading.Tasks;
using Abstract;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachine.Miner
{
    public class DigState : MinerStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private MinerManager _manager;

        #endregion

        #endregion

        public DigState(MinerManager manager  )
        {
            _manager = manager;
        }

        public void EnterState()
        {
            DigDiamond();
            _manager.SetTriggerAnim(MinerAnimType.Dig);
         
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
        }

        private  void DigDiamond()
        {
            _manager.StartCoroutine(_manager.DigDiamond());
        }

        public void SwitchState()
        {
            _manager.SwitchState(MinerStatesType.GoStack);
        }
    }
}