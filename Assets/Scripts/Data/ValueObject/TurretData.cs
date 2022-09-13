using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData : Buyable
    {
        public bool IsActive;
        public bool HasTurretSoldier;
        public int AmmoCapacity;
        public int AmmoDamage;

        public TurretData(PayType payType, int cost, int payedAmount) : base(payType, cost, payedAmount)
        {
        }
    }
}