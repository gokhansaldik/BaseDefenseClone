using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData : Buyable
    {
        // public bool IsActive;
        // public bool HasTurretSoldier;
        // public int AmmoCapacity;
        // public int AmmoDamage;
        // [Space]
        // public StackData BulletBoxStackData;
        // public int FireRate=1;
        // public int Damage=1;
        public int LimitX;
        public int LimitY;
        public int LimitZ;
        public float OffsetX;
        public float OffsetY;
        public float OffsetZ;
        public StackData BulletBoxStackData;
        public float FireRate=1;
        public int Damage=1;
        public int AutoModeCost=500;
    }
}