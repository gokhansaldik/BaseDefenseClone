using System;
using Abstract;
using Enums;
using Sirenix.OdinInspector;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomData : Buyable
    {
        public RoomNameType RoomName;
        public bool Isbase;
        
        public RoomData(PayType payType, int cost, int payedAmount) : base(payType, cost, payedAmount)
        {
        }

        public TurretData TurretData;
    }
}