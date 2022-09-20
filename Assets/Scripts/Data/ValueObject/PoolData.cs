using System;
using Enums;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public PoolType PoolType;
        public GameObject Pref;
        public int ObjectCount;
    }
}