using System;
using Controllers.Player;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using Data.UnityObject;
using UnityEngine;


namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public PlayerData PlayerData;
        public bool InBase = true;

        #endregion

        #region Seriliazed Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerStackController playerStackController;
        [SerializeField] private GameObject pistolGun;
        //[SerializeField] private Transform shootTransform;
        [SerializeField] private PlayerAimController playerAimController;

        #endregion

        #region Private Variables

        private PlayerData _playerData;
        private GameStatesType _states;
        [SerializeField] private HealthManager _healthManager;

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
            SendPlayerDataToControllers();
        }

        private void FixedUpdate()
        {
            BaseHealthUpgrade();
            _healthManager.healthImage.fillAmount = Convert.ToSingle(_healthManager.CurrentHealth) /
                                                    Convert.ToSingle(_healthManager.healthInfo.HealthData.maxHealth);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputTaken += OnPointerDown;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            InputSignals.Instance.onJoystickDragged += OnJoystickDragged;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
          //  IdleGameSignals.Instance.onGetPistolAmmo += OnGetPistolAmmo;
          EnemySignals.Instance.onEnemyDie += playerAimController.OnRemoveFromTargetList;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputTaken -= OnPointerDown;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onJoystickDragged -= OnJoystickDragged;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
           // IdleGameSignals.Instance.onGetPistolAmmo -= OnGetPistolAmmo;
           EnemySignals.Instance.onEnemyDie -= playerAimController.OnRemoveFromTargetList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnPointerDown()
        {
            ActivateMovement();
        }

        private void OnInputReleased()
        {
            DeactivateMovement();
        }


        private void OnJoystickDragged(IdleInputParams inputParams)
        {
            playerMovementController.UpdateIdleInputValue(inputParams);
        }


        public void DeactivateMovement()
        {
            playerMovementController.DeactivateMovement();
            ChangePlayerAnimation(PlayerAnimationStates.Idle);
            pistolGun.SetActive(false);
        }

        private void ActivateMovement()
        {
            playerMovementController.ActivateMovement();
            if (InBase) //InBase true ise
            {
                ChangePlayerAnimation(PlayerAnimationStates.Run);
                pistolGun.SetActive(false);
            }
            else if (!InBase)
            {
                ChangePlayerAnimation(PlayerAnimationStates.Gun);
                pistolGun.SetActive(true);
            }
        }

        public void ChangePlayerAnimation(PlayerAnimationStates animType)
        {
            playerAnimationController.ChangeAnimationState(animType);
        }

        private void SendPlayerDataToControllers()
        {
            playerStackController.SetStackData(PlayerData.StackData);
        }

        public void AddStack(GameObject obj)
        {
            playerStackController.MoneyAddStack(obj);
        }

        private void OnLevelFailed() => playerMovementController.IsReadyToPlay(false);

        private void OnReset()
        {
            playerMovementController.MovementReset();
            playerAnimationController.gameObject.SetActive(false);
            transform.DOScale(Vector3.one, .1f);
            playerMovementController.OnReset();
        }

        public void BaseHealthUpgrade()
        {
            if (InBase && _healthManager.CurrentHealth < 100)
            {
                _healthManager.CurrentHealth++;
            }
        }

        public void CollectableAddMine()
        {
            
        }

        // public void Shoot()
        // {
        //     IdleGameSignals.Instance.onGetPistolAmmo();
        // }
        //
        // private GameObject OnGetPistolAmmo()
        // {
        //     var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Ammo);
        //     if (obj == null)
        //     {
        //         return null;
        //     }
        //
        //     obj.transform.position = shootTransform.transform.position;
        //     obj.SetActive(true);
        //     return obj;
        // }
    }
}