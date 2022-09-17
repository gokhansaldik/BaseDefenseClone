using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data.ValueObject
{
    [Serializable]
    public class EnemyAiData
    {
        public List<EnemyTypeData> EnemyList;
        public List<Transform> SpawnPosList;
    }
}