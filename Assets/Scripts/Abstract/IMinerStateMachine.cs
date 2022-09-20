using UnityEngine;

namespace Abstract
{
    public interface IMinerStateMachine
    {
        void EnterState();
        void UpdateState();
        void CollisionState(Collider other);
        void SwitchState();
    }
}