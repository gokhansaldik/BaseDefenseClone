using System.Collections.Generic;
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

        #endregion

        #region Private Variables

        private PlayerStackData _playerStackData;
        private int _currentStackLevel;
        private float _directY;

        #endregion

        #endregion

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