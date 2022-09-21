using System.Collections;
using Abstract;
using Controllers.Miner;
using Enums;
using Interface;
using Signals;
using StateMachine.Miner;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public GameObject Target;
        public GameObject Stack;
        public IMinerStateMachine CurrentState;

        #endregion

        #region Serialized Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private MinerAnimationController animationController;
        [SerializeField] private GameObject diamond;
        [SerializeField] private GameObject pickaxe;

        #endregion

        #region Private Variables

        private GoDiggingState _goMine;
        private GoStackingState _goStacking;
        private PickaxeState _pickaxe;
        private WaitState _wait;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _pickaxe = new PickaxeState(this);
            _goStacking = new GoStackingState(this, ref agent);
            _goMine = new GoDiggingState(this, ref agent);
            _wait = new WaitState(this, ref agent);
            CurrentState = _goMine;
        }

        private void OnEnable()
        {
            Target = IdleGameSignals.Instance.onGetMineTarget();
            Stack = IdleGameSignals.Instance.onGetMineStackTarget();
            CurrentState.EnterState();
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }
        
        public void SwitchState(MinerStatesType stateType)
        {
            switch (stateType)
            {
                case MinerStatesType.Pickaxe:
                    CurrentState = _pickaxe;
                    diamond.SetActive(false);
                    pickaxe.SetActive(true);
                    break;
                case MinerStatesType.GoDigging:
                    CurrentState = _goMine;
                    diamond.SetActive(false);
                    break;
                case MinerStatesType.GoStacking:
                    CurrentState = _goStacking;
                    pickaxe.SetActive(false);
                    diamond.SetActive(true);
                    break;
                case MinerStatesType.Wait:
                    CurrentState = _wait;
                    break;
            }

            CurrentState.EnterState();
        }

        public void SetTriggerAnim(MinerAnimType animType)
        {
            animationController.SetAnim(animType);
        }

        public void SetAnimLayer(AnimationLayerType type, float weight)
        {
            animationController.SetLayer(type, weight);
        }

        public IEnumerator DigDiamond()
        {
            yield return new WaitForSeconds(5);
            SwitchState(MinerStatesType.GoStacking);
        }
    }
}