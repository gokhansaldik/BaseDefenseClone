using System.Collections.Generic;
using System.Threading.Tasks;
using Commands.Stack;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public StackData StackData;
        public bool LerpStatus;
        public List<GameObject> StackList = new List<GameObject>();

        #endregion
        
        #region Serialized Variables

        [SerializeField] private StackManager stackManager;
        
        #endregion

        #region Private Variables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private Transform _playerManager;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddInStack += OnAddInStack;
            StackSignals.Instance.onCollectablePlayerMiner += OnCollectablePlayerMiner;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }
        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onCollectablePlayerMiner -= OnCollectablePlayerMiner;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").StackData;
        }
        private void GetReferences()
        {
            StackData = GetStackData();
        }
        private void Init()
        {
            _collectableAddOnStackCommand = new CollectableAddOnStackCommand(ref stackManager, ref StackList, ref StackData);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref StackList);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();
        }
        private void Update()
        {
            if (LerpStatus)
            {
                _stackLerpMovementCommand.Execute(ref _playerManager);
                StackSignals.Instance.onCollectableUpSpeed.Invoke();
            }
            else if (!LerpStatus)
            {
                StackSignals.Instance.onCollectableUpDown.Invoke();
            }
        }
        private void AddInStack(GameObject obj)
        {
            _collectableAddOnStackCommand.Execute(obj);
        }
      
        private void FindPlayer()
        {
            if (!_playerManager) _playerManager = FindObjectOfType<PlayerManager>().transform;
        }
        private void OnAddInStack(GameObject obj)
        {
            AddInStack(obj);
            if (LerpStatus)
            {
                CollectableAnimSet(obj, CollectableAnimationStates.Taken);
            }
        }
        private void CollectableAnimSet(GameObject obj, CollectableAnimationStates animationStates)
        {
            _collectableAnimSetCommand.Execute(obj, animationStates);
        }
        private void OnCollectablePlayerMiner()
        {
            if (StackList.Count >= 0)
            {
                var lastHostage = StackList[0];
                StackList.Remove(lastHostage);
            }
        }
        private async void ClearStackManager()
        {
            for (var i = 0; i < StackList.Count; i++)
            {
                PoolSignals.Instance.onSendPool?.Invoke(stackManager.transform.GetChild(0).gameObject,
                    PoolType.Collectable);
            }
            StackList.Clear();
            StackList.TrimExcess();
            await Task.Delay(100);
        }
        private void OnPlay()
        {
            FindPlayer();
        }
        private void OnReset()
        {
            ClearStackManager();
        }
    }
}