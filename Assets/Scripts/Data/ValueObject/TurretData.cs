using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class TurretData : Buyable
    {
        public int LimitX;
        public int LimitZ;
        public float OffsetX;
        public float OffsetY;
        public float OffsetZ;
        
    }
}