using UnityEngine;

namespace Interface
{
    public interface IMinerStateMachine
    {
        void EnterState();
        void UpdateState();
        void CollisionState(Collider other);
        void SwitchState();
    }
}