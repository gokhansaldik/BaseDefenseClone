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
        public GameObject PistolGun;

        #endregion

        #region Seriliazed Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerStackController playerStackController;
        [SerializeField] private PlayerAimController playerAimController;
        [SerializeField] private HealthManager healthManager;

        #endregion

        #region Private Variables

        private PlayerData _playerData;

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
            healthManager.HealthImage.fillAmount = Convert.ToSingle(healthManager.CurrentHealth) /
                                                    Convert.ToSingle(healthManager.HealthInfo.HealthData.maxHealth);
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
        private void DeactivateMovement()
        {
            playerMovementController.DeactivateMovement();
            ChangePlayerAnimation(PlayerAnimationStates.Idle);
            PistolGun.SetActive(false);
        }
        private void ActivateMovement()
        {
            playerMovementController.ActivateMovement();
            if (InBase)
            {
                ChangePlayerAnimation(PlayerAnimationStates.Run);
                PistolGun.SetActive(false);
            }
            else if (!InBase)
            {
                ChangePlayerAnimation(PlayerAnimationStates.Gun);
                PistolGun.SetActive(true);
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
        private void BaseHealthUpgrade()
        {
            if (InBase && healthManager.CurrentHealth < 100)
            {
                healthManager.CurrentHealth++;
            }
        }
    }
}