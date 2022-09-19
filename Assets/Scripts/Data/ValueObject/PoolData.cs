using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public PoolType Type;
        public GameObject Pref;
        public int ObjectCount;
    }
}