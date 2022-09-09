using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomData 
    {
        public BaseTurretData TurretData;
        public int RoomCost;
        public int RoomPayedAmount;
    }
}