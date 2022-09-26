using System;
using System.Collections.Generic;
using Commands.Stack;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Player
{
    public class PlayerStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> MoneyStackList;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject moneyStackHolder;
        [SerializeField] private PlayerStackController playerStackController;
        [SerializeField] private Transform ammoStackHolder;

        #endregion

        #region Private Variables

        private StackData _data;
        private PlayerStackData _playerStackData;
        private int _currentStackLevel;
        private float _directY;
        private AmmoStackCommand _ammoStackCommand;
        private List<GameObject> _stackList=new List<GameObject>();
        
        #endregion

        #endregion

        private void Start()
        {
            _stackList = new List<GameObject>();
            _ammoStackCommand = new AmmoStackCommand(ref playerStackController,ref _stackList,ref ammoStackHolder,ref _data.AmmoStackData);
        }

        public void SetStackData(PlayerStackData data)
        {
            _playerStackData = data;
        }


        public void MoneyAddStack(GameObject obj)
        {
            if (obj == null) return;
            MoneyStackList.Add(obj);
            obj.transform.SetParent(moneyStackHolder.transform);
            SetObjectPosition(obj);
        }
      


        private void SetObjectPosition(GameObject obj)
        {
            obj.transform.DOLocalRotate(Vector3.zero, _playerStackData.AnimationDurition);
            obj.transform.DOLocalMove(new Vector3(0, _directY, -(_currentStackLevel * _playerStackData.StackoffsetZ +0.05f)),_playerStackData.AnimationDurition);
            _directY = MoneyStackList.Count % _playerStackData.StackLimit * _playerStackData.StackoffsetY;
            _currentStackLevel = MoneyStackList.Count / _playerStackData.StackLimit;
        }
        public void MoneyLeaving(GameObject target)
        {
            int limit = MoneyStackList.Count;
            for (int i = 0; i < limit; i++)
            {
                var obj = MoneyStackList[0];
                MoneyStackList.RemoveAt(0);
                MoneyStackList.TrimExcess();
                obj.transform.parent = target.transform;
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.2f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj,
                        PoolType.Money);
                });

                ScoreSignals.Instance.onSetScore?.Invoke(PayType.Money, MoneyStackList.Count);
                SaveSignals.Instance.onScoreSave?.Invoke();
            }
        }
        public void BulletBoxLeaving(GameObject target)
        {
            int limit = MoneyStackList.Count;
            for (int i = 0; i < limit; i++)
            {
                var obj = MoneyStackList[0];
                MoneyStackList.RemoveAt(0);
                MoneyStackList.TrimExcess();
                obj.transform.parent = target.transform;
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.2f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj,
                        PoolType.Bullet);
                });

               // ScoreSignals.Instance.onSetScore?.Invoke(PayType.Bullet, MoneyStackList.Count);
                
            }
        }
    }
}