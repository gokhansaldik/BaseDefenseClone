using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseTurretData
    {
        public int TurretDamage;
        public int TurretCapacity;
        public bool HasSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public bool IsActive;
    }
}