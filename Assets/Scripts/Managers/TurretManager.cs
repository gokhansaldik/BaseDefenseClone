using System.Collections.Generic;
using Commands.Stack;
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

        public bool HasOwner = false;
        public bool IsPlayerUsing = false;
        public GameObject PlayerHandle;
        public List<Transform> AmmoBoxList = new List<Transform>();
        public TurretStateType TurretType = TurretStateType.None;
        #endregion

        #region Serialized Variables
        [SerializeField] private GameObject turret;
       // [SerializeField] private TurretRangeController rangeController;

        [SerializeField] private Transform turretPlayerParentObj;
        [SerializeField] private Transform turretRotatableObj;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject ownerObject;
        [SerializeField] private TurretMovementController turretMovementController;
        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        //private TurretStackSetPosCommand _turretStackSetPosCommand;
        
        #endregion

        #region Private Variables

        private float _xValue, _zValue;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            _bulletBoxList= new List<GameObject>();
            Init();
            _data = GetTurretData();
        }

        private void Init()
        {
            //var manager = this;
            //_turretStackSetPosCommand = new TurretStackSetPosCommand(ref _bulletBoxList, ref _data);
            //_turretBulletBoxAddCommand = new TurretBulletBoxAddCommand(ref _bulletBoxList, ref _data, ref stackHolder, ref manager);
        }
        // private void TurretRotation()
        // {
        //     turret.transform.rotation.y 
        // }

        // public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" +
        //                                                           (LevelSignals.Instance.onGetCurrentModdedLevel() + 1)
        //                                                           .ToString());

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //PlayerSignals.Instance.onEnemyDie += rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onJoystickDragged += OnInputDragged;
            IdleGameSignals.Instance.onAddBulletBoxStack += OnAddBulletBoxStack;
            //IdleGameSignals.Instance.onPlayerInTurret += OnPlayerInTurret;
            //IdleGameSignals.Instance.onPlayerOutTurret += OnPlayerOutTurret;
        }

        private void UnsubscribeEvents()
        {
            //PlayerSignals.Instance.onEnemyDie -= rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onJoystickDragged -= OnInputDragged;
            //IdleGameSignals.Instance.onPlayerInTurret -= OnPlayerInTurret;
            //IdleGameSignals.Instance.onPlayerOutTurret -= OnPlayerOutTurret;
            IdleGameSignals.Instance.onAddBulletBoxStack -= OnAddBulletBoxStack;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;
        public void PlayerUseTurret(Transform player)
        {
            IsPlayerUsing = true;
            playerObject.SetActive(false);
            player.parent = turretPlayerParentObj;
            //player.transform.DOMove(turretOwner.position, 1f);
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
            //turretMovementController.SetTurnValue(data);
            // _xValue = data.XValue;
            // _zValue = data.ZValue;
            _xValue = data.JoystickMovement.x;
            _zValue = data.JoystickMovement.z;
        }

        private void FixedUpdate()
        {
            if (!IsPlayerUsing)
            {
                return;
            }
            if (_xValue.Equals(0))
            {
                return;
            }
            if (_xValue < -0.9f)
            {
                PlayerSignals.Instance.onPlayerUseTurret?.Invoke(false);
            }
            turretRotatableObj.rotation = Quaternion.Euler(new Vector3(0, 30 * _xValue * -1, 0));
        }
        // private void OnPlayerInTurret(GameObject target)
        // {
        //     if (target==gameObject)
        //     {
        //         TurretType = TurretStateType.PlayerIn;
        //     }
        // }
        // private void OnPlayerOutTurret(GameObject IsCheck)
        // {
        //     if (TurretType==TurretStateType.PlayerIn && IsCheck==gameObject)
        //     {
        //         TurretType = TurretStateType.None;
        //     }
        // }
        public void OnAddBulletBoxStack(GameObject target)
        {
           
          
            SetObjPosition(target);
            
        }
        public void SetObjPosition(GameObject bulletBox)
        {
            //_turretStackSetPosCommand.Execute(bulletBox);
            _directX = ((AmmoBoxList.Count % _data.LimitX)) * _data.OffsetX;
                _directY = (AmmoBoxList.Count / (_data.LimitX * _data.LimitZ)) * _data.OffsetY;
                _directZ = ((AmmoBoxList.Count % (_data.LimitX * _data.LimitZ)) / _data.LimitX) * _data.OffsetZ;
                bulletBox.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
            
        }

       
    }
   
}