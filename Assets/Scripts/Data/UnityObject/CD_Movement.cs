using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Data.UnityObject
{
    public abstract class CD_Movement : ScriptableObject
    {
        public abstract void DoMovement( ref Rigidbody _rigidbody,
            ref InputParams inputparams, ref PlayerMovementData _moveData);


        
    }
}