using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerMovementData playerMovementData;
}

[Serializable]
public class PlayerMovementData
{
    [Space] [Header("Idle")] public float IdleSpeed = 8f;
    public int PlayerHealth;
    public float AttackRange;
    //public float IdleTurnSpeed = .5f;
}