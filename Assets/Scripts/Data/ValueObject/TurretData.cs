using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData : Buyable
    {
        public bool IsActive;
        public bool HasTurretSoldier;
        public int AmmoCapacity;
        public int AmmoDamage;
    }
}