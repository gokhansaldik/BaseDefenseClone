using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Controllers.Worker.Ammo
{
    public class AmmoWorkerStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> AmmoStackList;
        [SerializeField] private GameObject ammoStackHolder;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private WorkerStackData _workerData;
        private float _directY;
        private int _currentStackLevel;

        #endregion

        #endregion

        public void MoneyAddStack(GameObject obj)
        {
            if (AmmoStackList.Count < 10)
            {
                if (obj == null) return;
                AmmoStackList.Add(obj);
                obj.transform.SetParent(ammoStackHolder.transform);
                SetObjectPosition(obj);
            }
        }

        public void SetObjectPosition(GameObject obj)
        {
            obj.transform.DOLocalRotate(Vector3.zero, _workerData.AnimationDurition);
            obj.transform.DOLocalMove(new Vector3(3, _directY, -(_currentStackLevel * _workerData.StackoffsetZ + 50f)),
                _workerData.AnimationDurition);
            _directY = AmmoStackList.Count % _workerData.StackLimit * _workerData.StackoffsetY;
            _currentStackLevel = AmmoStackList.Count / _workerData.StackLimit;
        }
    }
}