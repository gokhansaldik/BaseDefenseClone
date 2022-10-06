using Controllers.Collectable;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableAnimationController collectableAnimationController;
        public bool IsTaken = false;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onCollectablePlayerTaken += OnCollectablePlayerTaken;
            StackSignals.Instance.onCollectableUpSpeed += SetUpSpeedCollectable;
            StackSignals.Instance.onCollectableUpDown += SetDownSpeedCollectable;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onCollectablePlayerTaken -= OnCollectablePlayerTaken;
            StackSignals.Instance.onCollectableUpSpeed -= SetUpSpeedCollectable;
            StackSignals.Instance.onCollectableUpDown -= SetDownSpeedCollectable;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.PlayAnim(states);
        }

        public void SetAnimState(CollectableAnimationStates states)
        {
            collectableAnimationController.SetAnimState(states);
        }
        private void OnCollectablePlayerTaken()
        {
            if (IsTaken ==true)
            {
                SetAnimState(CollectableAnimationStates.Taken);
            }   
           
        }

        public void SetUpSpeedCollectable()
        {
            
                collectableAnimationController.SetSpeedVariable(1f);
            
            
        }
        public void SetDownSpeedCollectable()
        {
            
                collectableAnimationController.SetSpeedVariable(0f);
            
           
        }
        
    }
}