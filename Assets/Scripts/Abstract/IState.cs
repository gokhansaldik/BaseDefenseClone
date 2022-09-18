using UnityEngine;

namespace Abstract
{
    public interface IState
    {
        void UpdateIState();

        void OnEnter();

        void OnExit();
        void EnterState();
        void UpdateState();
        void OnCollisionDetectionState(Collider other);
        void SwitchState();
    }
}