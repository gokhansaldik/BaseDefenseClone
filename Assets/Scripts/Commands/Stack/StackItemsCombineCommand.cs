using System.Collections.Generic;
using System.Threading.Tasks;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StackItemsCombineCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private StackData _stackData;

        #endregion

        #endregion

        public StackItemsCombineCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stackList = stackList;
            _stackData = stackData;
        }

        public async void Execute()
        {
            var count = _stackList.Count;
            for (var i = 0; i < count; i++)
                if (i <= _stackData.StackLimit)
                {
                    _stackList[0].transform.DOScale(Vector3.zero, 0.12f);
                    Debug.Log(i);
                    await Task.Delay(150);
                    _stackList[0].SetActive(false);
                    PoolSignals.Instance.onSendPool?.Invoke(_stackList[0], PoolType.Collectable);
                    _stackList.RemoveAt(0);
                    _stackList.TrimExcess();
                }
                else
                {
                    _stackList[0].SetActive(false);
                    PoolSignals.Instance.onSendPool?.Invoke(_stackList[0], PoolType.Collectable);
                    _stackList.RemoveAt(0);
                    _stackList.TrimExcess();
                }
        }
    }
}