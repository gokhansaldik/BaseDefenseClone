using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;
using System.Collections;
using Controllers.Worker.Ammo;
using DG.Tweening;
using Inheritance;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        //[SerializeField] private AiBase aiBase;
        [SerializeField] private AmmoWorkerStackController ammoWorkerStackController;
        #endregion

        #region Private Variables
        private float _speed = 0.2f;
        private int _indeks = 0;
        private List<int> _openedTurrets = new List<int>();
        private List<Transform> _waysOnScene = new List<Transform>();
        private List<Vector3> _selectedWay = new List<Vector3>();

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
            // _waysOnScene.Add(GameObject.FindGameObjectWithTag("0way").transform);
            _workerData = GetWorkerData();

        }
        private WorkerData GetWorkerData() => Resources.Load<CD_AmmoWorker>("Data/CD_AmmoWorker").WorkerData;

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
            transform.DOMove(_ammoManager.position, 4 * _speed).SetSpeedBased(true).OnComplete(GoToTurret).SetEase(Ease.Linear);
            transform.DOLookAt(_ammoManager.position, 1);
        }
        private void GoToTurret()
        {
            SelectWay();
           
            for (int i = 0; i < _selectedWayObject.childCount; i++)
            {
                _selectedWay.Add(_selectedWayObject.GetChild(i).position);

            }
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoBackToAmmoManager);
        }

        private void GoBackToAmmoManager()
        {

            _selectedWay.Reverse();
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToAmmoManager);
            _indeks++;
            if (_indeks >= _openedTurrets.Count)
            {
                _indeks = 0;
            }
        }
        public void AddStack(GameObject obj) =>ammoWorkerStackController.MoneyAddStack(obj);
        private void SelectWay()
        {
            _selectedWay.Clear();
            _selectedWayObject = _waysOnScene[_openedTurrets[_indeks] + 1]; //+1 ekliyoruz ��nk� oyun ba��nda a��k olan taret numaras�z, di�erleri ise indeks 0'dan ba�layarak kaydediliyor.
        }

        private void OnBuyTurrets(int turretId)
        {
            this._openedTurrets.Add(turretId);
        }
        // private void OnTriggerStay(Collider other)
        // {
        //     if (other.CompareTag("AmmoArea"))
        //     {
        //        
        //         if (_timer >= 10)
        //         {
        //             playerManager.AddStack(IdleGameSignals.Instance.onGetAmmo());
        //             _timer = _timer * 60 / 100;
        //         }
        //         else
        //         {
        //             _timer++;
        //         }
        //     }
        // }

        

    }
}