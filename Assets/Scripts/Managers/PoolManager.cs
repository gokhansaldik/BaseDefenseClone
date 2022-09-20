using Commands.Pool;
using Data.UnityObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform poolManagerTransform;

        #endregion

        #region Private Variables

        private CD_Pool _cdPool;
        private GameObject _emptyGameObject;
        private PoolAddCommand _poolAddCommand;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPool += OnSendPool;
        }

        private void UnSubscribeEvent()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPool -= OnSendPool;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
            StartPool();
        }

        private void GetReferences()
        {
            _cdPool = GetPoolData();
        }

        private void Init()
        {
            _poolAddCommand = new PoolAddCommand(ref _cdPool, ref poolManagerTransform, ref _emptyGameObject);
        }


        private void StartPool()
        {
            _poolAddCommand.Execute();
        }

        private CD_Pool GetPoolData()
        {
            return Resources.Load<CD_Pool>("Data/CD_PoolGenerator");
        }

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var parent = transform.GetChild((int)poolType);
            var obj = parent.childCount != 0 ? parent.transform.GetChild(0).gameObject : null;
            return obj;
        }

        private void OnSendPool(GameObject poolObject, PoolType poolType)
        {
            poolObject.SetActive(false);
            poolObject.transform.localPosition = Vector3.zero;
            poolObject.transform.parent = transform.GetChild((int)poolType);
        }
    }
}