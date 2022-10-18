using System.Collections.Generic;
using Signals;
using UnityEngine;
using DG.Tweening;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private int _speed = 1;
        private int _indeks = 0;
        private List<int> _openedTurrets = new List<int>();
        private List<Transform> _waysOnScene = new List<Transform>();
        private List<Vector3> _selectedWay = new List<Vector3>();

        private Transform _selectedWayObject;
        private Transform _ammoManager;

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
        }

        private void Start()
        {
            GoToAmmoManager();
        }


        private void GoToAmmoManager()
        {
            transform.DOMove(_ammoManager.position, 4 * _speed).SetSpeedBased(true).OnComplete(GoToTurret)
                .SetEase(Ease.Linear);
            transform.DOLookAt(_ammoManager.position, 1);
        }

        private void GoToTurret()
        {
            SelectWay();

            for (int i = 0; i < _selectedWayObject.childCount; i++)
            {
                _selectedWay.Add(_selectedWayObject.GetChild(i).position);
            }

            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true)
                .SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoBackToAmmoManager);
        }

        private void GoBackToAmmoManager()
        {
            _selectedWay.Reverse();
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true)
                .SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToAmmoManager);

            _indeks++;
            if (_indeks >= _openedTurrets.Count)
            {
                _indeks = 0;
            }
        }

        private void SelectWay()
        {
            _selectedWay.Clear();
            _selectedWayObject =
                _waysOnScene
                    [_openedTurrets[_indeks] + 1];
        }
    }
}