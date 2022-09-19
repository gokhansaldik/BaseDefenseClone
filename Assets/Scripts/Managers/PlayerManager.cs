using Controllers;
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

        #endregion

        #region Seriliazed Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerStackController playerStackController;

        #endregion

        #region Private Variables

        private PlayerData _playerData;
        private GameStatesType _states;

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            //CoreGameSignals.Instance.onChangeGameState += OnGameStateChange;
            CoreGameSignals.Instance.onReset += OnReset;

            InputSignals.Instance.onInputTaken += OnPointerDown;
            InputSignals.Instance.onInputReleased += OnInputReleased;

            InputSignals.Instance.onJoystickDragged += OnJoystickDragged;

            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            //CoreGameSignals.Instance.onChangeGameState -= OnGameStateChange;
            CoreGameSignals.Instance.onReset -= OnReset;

            InputSignals.Instance.onInputTaken -= OnPointerDown;
            InputSignals.Instance.onInputReleased -= OnInputReleased;

            InputSignals.Instance.onJoystickDragged -= OnJoystickDragged;

            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetPlayerDataToControllers()
        {
            movementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
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
            movementController.UpdateIdleInputValue(inputParams);
        }


        public void DeactivateMovement()
        {
            movementController.DeactivateMovement();

            ChangePlayerAnimation(PlayerAnimationStates.Idle);
        }

        private void ActivateMovement()
        {
            movementController.ActivateMovement();
            ChangePlayerAnimation(PlayerAnimationStates.Run);
        }

        public void ChangePlayerAnimation(PlayerAnimationStates animType)
        {
            animationController.ChangeAnimationState(animType);
        }

        private void SendPlayerDataToControllers()
        {
            playerStackController.SetStackData(PlayerData.StackData);
        }

        public void AddStack(GameObject obj)
        {
            playerStackController.MoneyAddStack(obj);
            //playerStackController.DiamondAddStack(obj);
        }

        


        private void OnLevelFailed() => movementController.IsReadyToPlay(false);

        private void OnReset()
        {
            movementController.MovementReset();
            animationController.gameObject.SetActive(false);
            transform.DOScale(Vector3.one, .1f);
            movementController.OnReset();
        }
    }
}