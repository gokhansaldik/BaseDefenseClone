using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initalPosition;
        private Animator _animator;
        private CameraStatesType _cameraStatesType = CameraStatesType.Idle;
        private Transform _playerManager;
        [SerializeField] Transform _turretOwnerTransform;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }
        private void Start()
        {
            SetPlayerFollow();
        }
        private void GetReferences()
        {
            _animator = GetComponent<Animator>();
        }
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            PlayerSignals.Instance.onPlayerUseTurret += OnPlayerUseTurret;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            PlayerSignals.Instance.onPlayerUseTurret -= OnPlayerUseTurret;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void GetInitialPosition()
        {
            _initalPosition = transform.GetChild(0).localPosition;
        }
        private void MoveToInitialPosition()
        {
            transform.GetChild(0).localPosition = _initalPosition;
        }
        private void SetPlayerFollow()
        {
            _playerManager = FindObjectOfType<PlayerManager>().transform;
            OnSetCameraTarget(_playerManager);
        }
        private void OnSetCameraTarget(Transform _target)
        {
            stateDrivenCamera.Follow = _target;
        }
        private void OnPlay()
        {
            SetPlayerFollow();
            GetInitialPosition();
        }
        private void OnReset()
        {
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            MoveToInitialPosition();
        }

        private void OnPlayerUseTurret(bool turret)
        {
            if (turret)
            {
                OnSetCameraTarget(_turretOwnerTransform);
            }
            else
            {
                OnSetCameraTarget(_playerManager);
            }
        }
    }
}