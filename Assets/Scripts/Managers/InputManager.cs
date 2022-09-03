using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using UnityEngine;
using Signals;
using UnityEngine.PlayerLoop;

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
        [SerializeField] private  FloatingJoystick floatingJoystick;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] private GameStatesType currentGameStatesType;

        #endregion

        #region Private Variables

        private bool _isTouching = true;
        private float _currentVelocity;
        private Vector3 _joystickPosition;
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
                new DuringOnDraggingJoystickCommand(ref _joystickPosition, ref _moveVector, ref floatingJoystick);
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
                floatingJoystick.gameObject.SetActive(true);
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
        private void Update()
        {
            if (!isReadyForTouch) return;
            // if (Input.GetMouseButton(0))
            //     if (_isTouching)
            //     {
            //         if (isJoystick)
            //         {
            //             _duringOnDraggingJoystickCommand.Execute();
            //         }
            //         // else
            //         // {
            //         //     if (MousePosition != null) _duringOnDraggingCommand.Execute();
            //         // }
            //     }
            if (currentGameStatesType == GameStatesType.Idle)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_isTouching)
                    {
                        
                            _joystickPosition = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
                            
                            _moveVector = _joystickPosition;
                            
                            InputSignals.Instance.onJoystickDragged?.Invoke(new IdleInputParams()
                            {
                                joystickMovement = _moveVector
                            });
                        
                    }
                }
            
        }
    }
}
    
}