using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableRemoveOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly List<GameObject> _stackList;
        private readonly StackManager _manager;
        private readonly StackData _stackData;

        #endregion

        #endregion

        public CollectableRemoveOnStackCommand(ref List<GameObject> stackList, ref StackManager manager,ref StackData stackData)
        {
            _stackList = stackList;
            _manager = manager;
            _stackData = stackData;
        }

        public void Execute(GameObject collectableGameObject)
        {
            _stackList.Remove(collectableGameObject);
            _stackList.TrimExcess();
            if (_stackList.Count >= _stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit - 1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit - 1], CollectableAnimationStates.Run);
            }

            if (_stackList.Count == 0) LevelSignals.Instance.onLevelFailed?.Invoke();
            PoolSignals.Instance.onSendPool?.Invoke(collectableGameObject, PoolType.Collectable);
        }
    }
}