using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private GameStatesType currentGameState;
        [SerializeField] private StackManager _stackManager;
        
        #endregion

        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity;
        private Vector2? _mousePosition;
        private Vector3 _moveVector;
        private Vector3 _joystickPosition;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (!isReadyForTouch) return;

            if (Input.GetMouseButtonUp(0))
            {
                
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
                _stackManager.LerpOk = false;

                //_stackManager.CollectableAnimSet(obj, CollectableAnimationStates.Idle);
            }

            if (Input.GetMouseButtonDown(0))
            {
                
                    _isTouching = true;
                    InputSignals.Instance.onInputTaken?.Invoke();
                    if (!isFirstTimeTouchTaken)
                    {
                        isFirstTimeTouchTaken = true;
                        InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                    }

                    _mousePosition = Input.mousePosition;
                
                
            }


            if ((currentGameState == GameStatesType.Idle))
            {
                if (Input.GetMouseButton(0))
                {
                    _stackManager.LerpOk = true;

                    //_stackManager.CollectableAnimSet(obj, CollectableAnimationStates.Run);
                    if (_isTouching)
                    {
                        if (currentGameState == GameStatesType.Idle)
                        {
                            _joystickPosition = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);

                            _moveVector = _joystickPosition;

                            InputSignals.Instance.onJoystickDragged?.Invoke(new IdleInputParams()
                            {
                                JoystickMovement = _moveVector
                            });
                        }
                    }
                }
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
    }
}