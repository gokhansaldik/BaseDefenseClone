using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed = 5;
        public float SidewaysSpeed = 2;
        public float PlayerJoystickSpeed = 3;
        public float IdleSpeed = 8f;
        public float IdleTurnSpeed = .5f;
    }
}