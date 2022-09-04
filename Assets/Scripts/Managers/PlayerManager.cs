using Controllers;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using System;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Seriliazed Field

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;

        [SerializeField] private PlayerAnimationController animationController;

        #endregion Seriliazed Field

        #region Private

        private PlayerData _playerData;

        #endregion Private

        #endregion Self Variables

        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
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

        #endregion Event Subsicription

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


        private void OnJoystickDragged(IdleInputParams inputParams) =>
            movementController.UpdateIdleInputValue(inputParams);

        //private void OnGameStateChange(GameStates gameState) => movementController.ChangeGameStates(gameState);

        


        private void ActivateMovement()
        {
            movementController.ActivateMovement();
        }

        public void DeactivateMovement()
        {
            movementController.DeactivateMovement();
        }


      

        

        private Transform OnGetPlayerTransform() => transform;

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