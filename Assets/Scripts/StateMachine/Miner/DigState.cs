using System.Collections;
using System.Threading.Tasks;
using Abstract;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class DigState : IState
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

        public DigState(MinerManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
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
            DigDiamond();
            _manager.SetAnim(MinerAnimType.Dig);
            _manager.Axe.SetActive(true);
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
        }

        private async void DigDiamond()
        {
            await Task.Delay(7000);
            SwitchState();
        }

        public void SwitchState()
        {
          //  _manager.SwitchState(_manager.GoStack);
        }
    }
}