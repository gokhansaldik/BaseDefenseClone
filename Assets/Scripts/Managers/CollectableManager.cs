using Controllers.Collectable;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsTaken = false;
        
        #endregion
        
        #region Serialized Variables

        [SerializeField] private CollectableAnimationController collectableAnimationController;

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
        public void SetAnimState(CollectableAnimationStates states)
        {
            collectableAnimationController.SetAnimState(states);
        }
        private void OnCollectablePlayerTaken()
        {
            if (IsTaken)
            {
                SetAnimState(CollectableAnimationStates.Taken);
            }
        }
        private void SetUpSpeedCollectable()
        {
            collectableAnimationController.SetSpeedVariable(1f);
        }
        private void SetDownSpeedCollectable()
        {
            collectableAnimationController.SetSpeedVariable(0f);
        }
    }
}