using System;
using System.Collections.Generic;
using Commands.Stack;
using Data.ValueObject;
using DG.Tweening;
using UnityEngine;

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
    }
}