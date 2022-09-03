using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CD_MovementList cdMovementList;
        [SerializeField] private GameStatesType states;

        #endregion

        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _colorAreaSpeed = 1;
        private Vector3 _inputValue;
        private Vector2 _clampValues;

        private InputParams _inputParams;
        private Vector3 _movementDirection;

        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _playerMovementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void ChangeStates(GameStatesType states)
        {
            this.states = states;
        }

        public void UpdateInputValue(InputParams inputParam)
        {
            _inputParams = inputParam;
        }

        public void UpdateIdleInputValue(IdleInputParams inputParam) =>
            _movementDirection = inputParam.joystickMovement;

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }


        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
            
                    if (states == GameStatesType.Idle)
                    {
                        IdleMove();
                    }
                }
            
                else
                {
                    Stop();
                }
            
            }
          
        }

        //// private void IdleMove()
        //// {
        ////     var velocity = rigidbody.velocity;
        ////     velocity = new Vector3(_movementDirection.x * _playerMovementData.ForwardSpeed, velocity.y,
        ////         _movementDirection.z * _playerMovementData.ForwardSpeed);
        ////     rigidbody.velocity = velocity;
        ////
        ////     Vector3 position;
        ////     position = new Vector3(rigidbody.position.x, (position = rigidbody.position).y, position.z);
        ////     rigidbody.position = position;
        ////
        ////     if (_movementDirection != Vector3.zero)
        ////     {
        ////         Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
        ////
        ////         transform.GetChild(0).rotation = toRotation;
        ////     }
        //// }
        private void IdleMove()
        {
            Vector3 velocity = rigidbody.velocity;
            velocity = new Vector3(_movementDirection.x * _playerMovementData.IdleSpeed, velocity.y,
                _movementDirection.z * _playerMovementData.IdleSpeed);
            rigidbody.velocity = velocity;

            if (_movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
                transform.rotation = toRotation;
                return;
            }
        }

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}