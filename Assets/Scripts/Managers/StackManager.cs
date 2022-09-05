using System.Collections.Generic;
using System.Threading.Tasks;
using Commands;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public StackData StackData;

        #endregion

        #region Serilazible Variables

        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Variables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        
        private CollectableRemoveOnStackCommand _collectableRemoveOnStackCommand;
        //private TransportInStack _transportInStack;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private StackItemsCombineCommand _stackItemsCombineCommand;
        private Transform _playerManager;

        [ShowInInspector] private List<GameObject> _stackList = new List<GameObject>();

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
            StackSignals.Instance.onRemoveInStack += _collectableRemoveOnStackCommand.Execute;
            //StackSignals.Instance.onTransportInStack += _transportInStack.Execute;
            StackSignals.Instance.onGetStackList += OnGetStackList;


            CoreGameSignals.Instance.onEnterFinish += OnEnterFinish;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= _collectableRemoveOnStackCommand.Execute;
            //StackSignals.Instance.onTransportInStack -= _transportInStack.Execute;
            StackSignals.Instance.onGetStackList -= OnGetStackList;


            CoreGameSignals.Instance.onEnterFinish -= OnEnterFinish;
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
            return Resources.Load<CD_Stack>("Data/CD_Stack").Data;
        }


        private void GetReferences()
        {
            StackData = GetStackData();
        }

      

        private void Init()
        {
            _collectableAddOnStackCommand =
                new CollectableAddOnStackCommand(ref stackManager, ref _stackList, ref StackData);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref _stackList, ref StackData);
            
            _collectableRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref _stackList, ref stackManager,
                ref StackData);
           // _transportInStack = new TransportInStack(ref _stackList, ref stackManager, ref StackData);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();


            _stackItemsCombineCommand =
                new StackItemsCombineCommand(ref _stackList, ref StackData, ref stackManager);
        }


        private void Update()
        {
            if (!_playerManager)
                return;
            _stackLerpMovementCommand.Execute(ref _playerManager);
        }

        public void AddInStack(GameObject obj)
        {
            _collectableAddOnStackCommand.Execute(obj);
        }

        public void CollectableAnimSet(GameObject obj, CollectableAnimationStates animationStates)
        {
            _collectableAnimSetCommand.Execute(obj, animationStates);
        }

        private void OnEnterFinish()
        {
            _stackItemsCombineCommand.Execute();
        }


        private void SetAllCollectableAnim(CollectableAnimationStates states)
        {
            foreach (var t in _stackList)
                CollectableAnimSet(t, states);
        }

        private void FindPlayer()
        {
            if (!_playerManager) _playerManager = FindObjectOfType<PlayerManager>().transform;
        }


        private void OnAddInStack(GameObject obj)
        {
            //StartCoroutine(_stackShackAnimCommand.Execute());
            CollectableAnimSet(obj, CollectableAnimationStates.Run);
            AddInStack(obj);
        }


        // private void Initialized()
        // {
        //     for (var i = 0; i < 6; i++)
        //     {
        //         var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
        //         obj.SetActive(true);
        //         _collectableAddOnStackCommand.Execute(obj);
        //     }
        //
        //
        //     SetAllCollectableAnim(CollectableAnimationStates.Crouch);
        // }


        private void OnGetStackList(GameObject _stackListObj)
        {
            CollectableAnimSet(_stackListObj, CollectableAnimationStates.Run);
            AddInStack(_stackListObj);
        }

       


        private async void ClearStackManager()
        {
            var _items = stackManager.transform.childCount;
            for (var i = 0; i < _stackList.Count; i++)
            {
                PoolSignals.Instance.onSendPool?.Invoke(stackManager.transform.GetChild(0).gameObject,
                    PoolType.Collectable);
            }

            _stackList.Clear();
            _stackList.TrimExcess();
            await Task.Delay(100);
            //Initialized();
        }

        private void OnPlay()
        {
            FindPlayer();
            SetAllCollectableAnim(CollectableAnimationStates.Run);
            //ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }


        private void OnReset()
        {
            ClearStackManager();
            //ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }
    }
}