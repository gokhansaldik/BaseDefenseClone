using System.Collections.Generic;
using Data.ValueObject;
using Datas.ValueObject;
using Managers;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly StackManager _stackManager;
        private readonly List<GameObject> _stackList;
        private readonly StackData _stackData;
        private PlayerManager _playerManager;

        #endregion
        #endregion

        public CollectableAddOnStackCommand(ref StackManager stackManager, ref List<GameObject> stackList,ref StackData stackData)
        {
            _stackList = stackList;
            _stackManager = stackManager;
            _stackData = stackData;
        }

        public void Execute(GameObject _obj)
        {
            _obj.transform.parent = _stackManager.transform;
            _stackList.Add(_obj);
            var pivot = _stackList[_stackList.Count - 1].transform.position;
            _obj.transform.localPosition = new Vector3(pivot.x, pivot.y, pivot.z - _stackData.StackOffset * _stackList.Count * 2);
        }
    }
}