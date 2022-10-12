using System.Collections.Generic;
using Data.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> targetList;
        [SerializeField] private GameObject diamondHolder;
        public TextMeshPro minerCountText;

        #endregion

        #region Private Variables

        private MineStackData _data;
        private readonly List<GameObject> _diamondList = new List<GameObject>();
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _data = GetMineData();
        }

        private MineStackData GetMineData()
        {
            return Resources.Load<CD_Mine>("Data/CD_MineData").MineStackData;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleGameSignals.Instance.onGetMineTarget += OnGetMineTarget;
            IdleGameSignals.Instance.onGetMineStackTarget += OnGetMineStackTarget;
            IdleGameSignals.Instance.onAddDiamondStack += OnAddDiamondStack;
        }

        private void UnSubscribeEvent()
        {
            IdleGameSignals.Instance.onGetMineTarget -= OnGetMineTarget;
            IdleGameSignals.Instance.onGetMineStackTarget -= OnGetMineStackTarget;
            IdleGameSignals.Instance.onAddDiamondStack -= OnAddDiamondStack;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private GameObject OnGetMineTarget()
        {
            return targetList[Random.Range(0, targetList.Count)];
        }

        private GameObject OnGetMineStackTarget()
        {
            return diamondHolder;
        }

        public void StartCollectDiamond(GameObject target)
        {
            var limit = _diamondList.Count;
            for (var i = 0; i < limit; i++)
            {
                var obj = _diamondList[0];
                _diamondList.RemoveAt(0);
                _diamondList.TrimExcess();
                obj.transform.parent = target.transform;
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.2f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj,
                        PoolType.Diamond);
                });
                ScoreSignals.Instance.onSetScore?.Invoke(PayType.Gem, _diamondList.Count);
            }

            SaveSignals.Instance.onScoreSave?.Invoke();
        }

        private void OnAddDiamondStack(GameObject target)
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Diamond);
            if (obj == null) return;
            obj.transform.parent = diamondHolder.transform;
            obj.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1,
                target.transform.position.z);
            SetObjPosition(obj);
            obj.SetActive(true);
            _diamondList.Add(obj);
        }

        private void SetObjPosition(GameObject obj)
        {
            _directX = _diamondList.Count % _data.LimitX * _data.OffsetX;
            _directY = _diamondList.Count / (_data.LimitX * _data.LimitZ) * _data.OffsetY;
            _directZ = _diamondList.Count % (_data.LimitX * _data.LimitZ) / _data.LimitX * _data.OffsetZ;
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
        }
    }
}