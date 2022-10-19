using System.Collections.Generic;
using Controllers.Turret;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool HasOwner;
        public bool IsPlayerUsing;
        public GameObject PlayerHandle;
        public List<Transform> AmmoBoxList = new List<Transform>();
        public TurretStateType TurretType = TurretStateType.None;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject turret;
        [SerializeField] private Transform turretPlayerParentObj;
        [SerializeField] private Transform turretRotatableObj;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject ownerObject;
        [SerializeField] private GameObject ammoBoxHolder;
        [SerializeField] private TurretShootRangeController turretShootRangeController;

        #endregion

        #region Private Variables

        private List<GameObject> _bulletBoxList;

        private TurretData _data;
        private float _xValue, _zValue;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetTurretData();
            _bulletBoxList = new List<GameObject>();
        }


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onJoystickDragged += OnInputDragged;
            IdleGameSignals.Instance.onAddBulletBoxStack += OnAddBulletBoxStack;

            EnemySignals.Instance.onEnemyDie += turretShootRangeController.OnRemoveFromTargetList;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onJoystickDragged -= OnInputDragged;

            IdleGameSignals.Instance.onAddBulletBoxStack -= OnAddBulletBoxStack;
            EnemySignals.Instance.onEnemyDie -= turretShootRangeController.OnRemoveFromTargetList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private TurretData GetTurretData()
        {
            return Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;
        }

        public void PlayerUseTurret(Transform player)
        {
            IsPlayerUsing = true;
            playerObject.SetActive(false);
            player.parent = turretPlayerParentObj;
            player.transform.position = turretPlayerParentObj.position;
            player.transform.rotation = turretPlayerParentObj.rotation;
            ownerObject.SetActive(true);
        }

        public void PlayerLeaveTurret(Transform player)
        {
            IsPlayerUsing = false;
            player.parent = null;
            ownerObject.SetActive(false);
            playerObject.SetActive(true);
        }

        public void OnInputDragged(IdleInputParams data)
        {
            _xValue = data.JoystickMovement.x;
            _zValue = data.JoystickMovement.z;
        }

        private void FixedUpdate()
        {
            if (!IsPlayerUsing) return;

            if (_xValue.Equals(0)) return;

            if (_xValue < -0.9f) PlayerSignals.Instance.onPlayerUseTurret?.Invoke(false);

            turretRotatableObj.rotation = Quaternion.Euler(new Vector3(0, 30 * _xValue * -1, 0));
        }


        public void OnAddBulletBoxStack(GameObject target)
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.BulletBox);
            if (obj == null) return;
            obj.transform.parent = ammoBoxHolder.transform;
            obj.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1,
                target.transform.position.z);
            SetObjPosition(obj);
            obj.SetActive(true);
        }


        public void SetObjPosition(GameObject bulletBox)
        {
            _directX = AmmoBoxList.Count % _data.LimitX * _data.OffsetX;
            _directY = AmmoBoxList.Count / (_data.LimitX * _data.LimitZ) * _data.OffsetY;
            _directZ = AmmoBoxList.Count % (_data.LimitX * _data.LimitZ) / _data.LimitX * _data.OffsetZ;
            bulletBox.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
        }
    }
}