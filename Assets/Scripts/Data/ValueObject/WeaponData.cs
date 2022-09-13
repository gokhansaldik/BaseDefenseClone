using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType WeaponType;
        public int Damage;
        public float AttackRate;
    }
}