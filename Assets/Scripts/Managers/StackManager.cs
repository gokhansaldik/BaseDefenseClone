using System.Collections.Generic;
using System.Threading.Tasks;
using Commands.Stack;
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
        public bool LerpOk;

        #endregion

        #region Serilazible Variables

        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Variables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private CollectableRemoveOnStackCommand _collectableRemoveOnStackCommand;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private Transform _playerManager;
        [ShowInInspector] private List<GameObject> _stackList = new List<GameObject>();
        private int _numOfItemsHolding = 0;
        private Stack<Transform> _moneyTransform = new Stack<Transform>();

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

            StackSignals.Instance.onGetStackList += OnGetStackList;

       
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= _collectableRemoveOnStackCommand.Execute;

            StackSignals.Instance.onGetStackList -= OnGetStackList;


           
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
            _collectableAddOnStackCommand =
                new CollectableAddOnStackCommand(ref stackManager, ref _stackList, ref StackData);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref _stackList);
            _collectableRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref _stackList, ref stackManager, ref StackData);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();
            
        }


        private void Update()
        {
            if (!_playerManager)
                return;
            if (LerpOk == true)
            {
                _stackLerpMovementCommand.Execute(ref _playerManager);
                SetAllCollectableAnim(CollectableAnimationStates.Run);
            }
            else if (LerpOk == false)
            {
                SetAllCollectableAnim(CollectableAnimationStates.Idle);
            }
        }

        public void AddInStack(GameObject obj)
        {
            _collectableAddOnStackCommand.Execute(obj);
        }

        public void SetAllCollectableAnim(CollectableAnimationStates states)
        {
            foreach (var t in _stackList)
                CollectableAnimSet(t, states);
        }

        public void CollectableAnimSet(GameObject obj, CollectableAnimationStates animationStates)
        {
            _collectableAnimSetCommand.Execute(obj, animationStates);
        }
        

        private void FindPlayer()
        {
            if (!_playerManager) _playerManager = FindObjectOfType<PlayerManager>().transform;
        }


        private void OnAddInStack(GameObject obj)
        {
            AddInStack(obj);
            if (LerpOk == true)
            {
                CollectableAnimSet(obj, CollectableAnimationStates.Run);
            }
        }


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