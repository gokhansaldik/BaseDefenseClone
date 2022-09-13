using System;

namespace Data.ValueObject
{
    [Serializable]
    public class MineBaseData
    {
        public int MaxWorkerAmount;
        public int CurrentWorkerAmount;
        public int DiamondCapacity;
        public int CurrentDiamondAmount;
        public int MineCartCapacity;
        
    }
}