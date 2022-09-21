using UnityEngine;

namespace Interface
{
    public interface IMover
    {
        void MoveAction(Vector3 direction, float moveSpeed);
    }
}