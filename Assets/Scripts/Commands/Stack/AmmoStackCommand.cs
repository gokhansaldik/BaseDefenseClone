using System.Collections.Generic;
using Controllers.Player;
using Data.ValueObject;

using DG.Tweening;
using UnityEngine;

namespace Commands.Stack
{
    public class AmmoStackCommand
    {
        #region Self Variables

        #region Private Variables

        private PlayerStackController _stackController;
        private List<GameObject> _stack;
        private Transform _stackHolder;
        private AmmoStackData _data;
        private float _directY;
        private float _directX;
        private float _directZ;


        #endregion

        #endregion

        public AmmoStackCommand(ref PlayerStackController stackController,ref List<GameObject> stack,ref Transform stackHolder,ref AmmoStackData data)
        {
            _stackController = stackController;
            _stack = stack;
            _stackHolder=stackHolder;
            _data = data;
        }
        
       
        public void Execute(GameObject obj)
        {
            _directY = _stack.Count % _data.LimitY * _data.OffsetY;
            _directX = _stack.Count / (_data.LimitY * _data.LimitZ) * _data.OffsetX;
            _directZ = -(_stack.Count % (_data.LimitY * _data.LimitZ) / _data.LimitY * _data.OffsetZ);
            obj.transform.DOLocalRotate(Vector3.zero, _data.AnimationDurition);
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
           
        }
    }
}