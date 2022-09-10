using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerMovementData playerMovementData;
    public PlayerStackData StackData;
}

[Serializable]
public class PlayerMovementData
{
    [Space] [Header("Idle")] public float IdleSpeed = 8f;
    public int PlayerHealth;
    public float AttackRange;
    //public float IdleTurnSpeed = .5f;
}
[Serializable]
public class PlayerStackData
{
    public int StackLimit = 10;
    public float StackoffsetY = 10;
    public float StackoffsetZ = 10;
    public float AnimationDurition = 1;
}