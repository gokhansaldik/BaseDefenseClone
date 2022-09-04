using Enums;
using Keys;
using DG.Tweening;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public
        
        #endregion

        #region Serialized

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private GameStatesType currentGameState;
        [SerializeField] private PlayerManager manager;
        
        #endregion

        #region Private

        private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay, _isMovingVertical;
        private float _inputValueX;
        private Vector2 _clampValues;
        private Vector3 _movementDirection;
        

        #endregion
        
        #endregion
        
        public void SetMovementData(PlayerMovementData movementData) => _movementData = movementData;
        public void ActivateMovement() => _isReadyToMove = true;
        public void DeactivateMovement() => _isReadyToMove = false;

       
        public void UpdateIdleInputValue(IdleInputParams inputParam) => _movementDirection = inputParam.joystickMovement;
        public void IsReadyToPlay(bool state) => _isReadyToPlay = state;
        //public void ChangeGameStates(GameStates currentState) => currentGameState = currentState;

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                   
                     if (currentGameState == GameStatesType.Idle)
                    {
                        IdleMove();
                    }
                }
                else
                {
                    
                     if (currentGameState == GameStatesType.Idle)
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
            velocity = new Vector3(_movementDirection.x * _movementData.IdleSpeed, velocity.y,
                _movementDirection.z * _movementData.IdleSpeed);
            rigidBody.velocity = velocity;

            if (_movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
                transform.rotation = toRotation;
                return;
            }
        }
        

        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            
            rigidBody.angularVelocity = Vector3.zero;
        }
       

       

        public  void MovementReset()
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
    }
}