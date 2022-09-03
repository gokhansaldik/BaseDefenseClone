using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        //[SerializeField] private GameObject scoreArea;
       // [SerializeField] private ParticleSystem colorParticle;

        #endregion

        #region Private Variables

        private bool _scoreAreaVisible = true;
        private GameStatesType _states;
        private int _score;
        private PlayerAnimationStates _animationState;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data.MovementData);
        }
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputTaken += playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased += playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onJoystickDragged += OnJoystickDragged;
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
           // CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
            // ScoreSignals.Instance.onSetPlayerScore += OnSetScore;
            //
            // StackSignals.Instance.onScaleSet += OnScaleSet;
            //
            // IdleGameSignals.Instance.onStageChanged += OnStageChanged;
        }


        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onJoystickDragged -= OnJoystickDragged;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            // CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
            // ScoreSignals.Instance.onSetPlayerScore -= OnSetScore;
            //
            // StackSignals.Instance.onScaleSet -= OnScaleSet;
            //
            // IdleGameSignals.Instance.onStageChanged -= OnStageChanged;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion
        private void Start()
        {
            _states = GameStatesType.Idle;
        }
        
        private void OnInputDragged(InputParams InputParam)
        {
            playerMovementController.UpdateInputValue(InputParam);
            PlayAnim(Mathf.Abs(InputParam.Values.x + InputParam.Values.y));
        }

        private void OnJoystickDragged(IdleInputParams idleInputParams) => playerMovementController.UpdateIdleInputValue(idleInputParams);
        private void PlayAnim(float value)
        {
            if (_states != GameStatesType.Idle) return;
            playerAnimationController.PlayAnim(value);
        }
        private void OnGetGameState(GameStatesType states)
        {
            _states = states;
            playerAnimationController.gameObject.SetActive(true);
            playerMovementController.ChangeStates(states);
        }
        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }
        private void OnPlay()
        {
           // scoreArea.SetActive(true);
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnReset()
        {
            playerMovementController.OnReset();
        }

    }
}