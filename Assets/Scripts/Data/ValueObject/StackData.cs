using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public int StackLimit = 10;
        public float StackOffset = 0.01f;
        public AmmoStackData AmmoStackData;
        public int LimitX;
        public int LimitY;
        public int LimitZ;
        [Space(15),Header("Offset")] 
        [Range(0f, 1f)] public float OffsetX;
        [Range(0f, 1f)] public float OffsetY;
        [Range(0f, 1f)] public float OffsetZ;
        [Space(15)]
         public float AnimationDurition = 1;
    }

    [Serializable]
    public class AmmoStackData
    {
        #region OffSet

        [Space(15), Header("Offset")] 
        [Range(0f, 1f)] public float OffsetX;

        [Range(0f, 1f)] public float OffsetY;
        [Range(0f, 1f)] public float OffsetZ;

        [Space(15)]

        #endregion

        #region Data

        [Header("Limit")]
        public int LimitX;
        public int LimitY;
        public int LimitZ;

        [Space(15)]

        #endregion

        public int StackLimit = 10;

        public float AnimationDurition = 1;
    }
}