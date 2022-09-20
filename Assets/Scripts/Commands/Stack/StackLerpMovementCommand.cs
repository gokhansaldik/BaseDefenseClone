using System.Collections.Generic;
using UnityEngine;

namespace Commands.Stack
{
    public class StackLerpMovementCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private List<Vector3> _positionHistory = new List<Vector3>();
        private int _distance = 10;
        private int _bodySpeed = 5;

        #endregion

        #endregion

        public StackLerpMovementCommand(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }

        public void Execute(ref Transform _playerTransform)
        {
            _positionHistory.Insert(0, _playerTransform.position);
            int index = 0;

            foreach (var body in _stackList)
            {
                Vector3 point = _positionHistory[Mathf.Min(index * _distance, _positionHistory.Count - 1)];
                Vector3 moveDirection = point - body.transform.position;
                body.transform.position += moveDirection * _bodySpeed * Time.deltaTime;
                body.transform.LookAt(point);
                index++;
            }
        }
    }
}