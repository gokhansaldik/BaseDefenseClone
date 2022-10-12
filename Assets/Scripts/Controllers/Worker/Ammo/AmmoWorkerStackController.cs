using System.Collections.Generic;
using Data.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Controllers.Worker.Ammo
{
    public class AmmoWorkerStackController : MonoBehaviour
    {
        public List<GameObject> AmmoStackList;
        [SerializeField] private GameObject ammoStackHolder;
        private WorkerStackData _workerData;
        private float _directY;
        private int _currentStackLevel;
        
        public void MoneyAddStack(GameObject obj)
        {
            if (AmmoStackList.Count <11)
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
            obj.transform.DOLocalMove(
                new Vector3(3, _directY, -(_currentStackLevel * _workerData.StackoffsetZ + 0.05f)),
                _workerData.AnimationDurition);
            _directY = AmmoStackList.Count % _workerData.StackLimit * _workerData.StackoffsetY;
            _currentStackLevel = AmmoStackList.Count / _workerData.StackLimit;
        }
    }
}
