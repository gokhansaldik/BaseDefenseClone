using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;
using Signals;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Vector3? MousePosition; //ref type

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isJoystick;
        [SerializeField] private Joystick joystick;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;

        #endregion

        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity;
        private Vector3 _joystickPos;
        private Vector3 _moveVector;
        private InputData _inputData;
        private DuringOnDraggingJoystickCommand _duringOnDraggingJoystickCommand;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void GetReferences()
        {
            _inputData = GetInputData();
        }

        private void Init()
        {
            _duringOnDraggingJoystickCommand =
                new DuringOnDraggingJoystickCommand(ref _joystickPos, ref _moveVector, ref joystick);
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").InputData;
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetGameState += OnGetGameStates;
        }

        private void Update()
        {
            if (!isReadyForTouch) return;
            if (Input.GetMouseButton(0))
                if (_isTouching)
                {
                    if (isJoystick)
                    {
                        _duringOnDraggingJoystickCommand.Execute();
                    }
                    // else
                    // {
                    //     if (MousePosition != null) _duringOnDraggingCommand.Execute();
                    // }
                }
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGetGameState -= OnGetGameStates;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        #endregion


        #region Subscriptions Methods

        private void OnGetGameStates(GameStatesType states)
        {
            if (states == GameStatesType.Idle)
            {
                joystick.gameObject.SetActive(true);
                isJoystick = true;
            }
            else
            {
                //dungeon kismi 
            }
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        #endregion
    }
}