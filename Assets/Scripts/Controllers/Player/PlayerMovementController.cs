using DG.Tweening;
using Enums;
using Keys;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        public Transform Target;
        
        #region Serialized Variables

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private GameStatesType gameStatesType;
        
        #endregion

        #region Private Variables
        
        private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove;
        private bool _isReadyToPlay;
        private Vector3 _movementDirection;

        #endregion
        #endregion
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    if (gameStatesType == GameStatesType.Idle)
                    {
                        IdleMove();
                    }
                }
                else
                {
                    if (gameStatesType == GameStatesType.Idle)
                    {
                        Stop();
                    }
                }
            }
            else
                Stop();
        }

        private void IdleMove()
        {
            Vector3 velocity = rigidBody.velocity;
            velocity = new Vector3(_movementDirection.x * _playerMovementData.IdleSpeed, velocity.y, _movementDirection.z * _playerMovementData.IdleSpeed);
            rigidBody.velocity = velocity;
            if (Target == null)
            {
                if (_movementDirection != Vector3.zero)
                {
                
                    Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
                    transform.rotation = toRotation;
                }
            }
            else
            {
                transform.LookAt(Target);
            }
        }
        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
        public void MovementReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
        public void OnReset()
        {
            DOTween.KillAll();
        }
        public void SetMovementData(PlayerMovementData movementData) => _playerMovementData = movementData;
        public void ActivateMovement()
        {
            _isReadyToMove = true;
        }
        public void DeactivateMovement()
        {
            _isReadyToMove = false;
        }
        public void UpdateIdleInputValue(IdleInputParams inputParam) => _movementDirection = inputParam.JoystickMovement;
        public void IsReadyToPlay(bool state) => _isReadyToPlay = state;
    }
}