using System;
using Abstract;
using Enums;


namespace Data.ValueObject
{
    [Serializable]
    public class RoomData : Buyable
    {
        public bool Base;
        public TurretData TurretData;
    }
}