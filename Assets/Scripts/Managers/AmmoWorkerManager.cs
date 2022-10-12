using System.Collections.Generic;
using Controllers.Worker.Ammo;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        
        [SerializeField] private AmmoWorkerStackController ammoWorkerStackController;

        #endregion

        #region Private Variables

        private readonly float _speed = 0.2f;
        private int _indeks;
        private readonly List<int> _openedTurrets = new List<int>();
        private readonly List<Transform> _waysOnScene = new List<Transform>();
        private readonly List<Vector3> _selectedWay = new List<Vector3>();
        private Transform _selectedWayObject;
        private Transform _ammoManager;
        private int _timer;
        private WorkerData _workerData;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _ammoManager = GameObject.FindGameObjectWithTag("AmmoArea").transform;
            _waysOnScene.Add(GameObject.FindGameObjectWithTag("0way").transform);
            // _waysOnScene.Add(GameObject.FindGameObjectWithTag("1way").transform);
            // _waysOnScene.Add(GameObject.FindGameObjectWithTag("2way").transform);
            // _waysOnScene.Add(GameObject.FindGameObjectWithTag("3way").transform);
            // _waysOnScene.Add(GameObject.FindGameObjectWithTag("4way").transform);
            _workerData = GetWorkerData();
        }

        private WorkerData GetWorkerData()
        {
            return Resources.Load<CD_AmmoWorker>("Data/CD_AmmoWorker").WorkerData;
        }

        private void Start()
        {
            GoToAmmoManager();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GoToAmmoManager()
        {
            transform.DOMove(_ammoManager.position, 4 * _speed).SetSpeedBased(true).OnComplete(GoToTurret)
                .SetEase(Ease.Linear);
            transform.DOLookAt(_ammoManager.position, 1);
        }

        private void GoToTurret()
        {
            SelectWay();
            for (var i = 0; i < _selectedWayObject.childCount; i++)
                _selectedWay.Add(_selectedWayObject.GetChild(i).position);

            transform.DOPath(_selectedWay.ToArray(), 4 * _speed).SetSpeedBased(true)
                .SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoBackToAmmoManager);
        }

        private void GoBackToAmmoManager()
        {
            _selectedWay.Reverse();
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed).SetSpeedBased(true)
                .SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToAmmoManager);
            _indeks++;
            if (_indeks >= _openedTurrets.Count) _indeks = 0;
        }

        public void AddStack(GameObject obj)
        {
            ammoWorkerStackController.MoneyAddStack(obj);
        }

        private void SelectWay()
        {
            _selectedWay.Clear();
            _selectedWayObject = _waysOnScene[_openedTurrets[_indeks] + 1];
        }

        private void OnBuyTurrets(int turretId)
        {
            _openedTurrets.Add(turretId);
        }
    }
}