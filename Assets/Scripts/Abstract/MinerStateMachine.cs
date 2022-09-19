using UnityEngine;

namespace Abstract
{
    public interface MinerStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnCollisionDetectionState(Collider other);
        void SwitchState();
    }
}