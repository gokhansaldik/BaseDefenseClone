using System;
using Controllers.Enemy;
using Random = UnityEngine.Random;

namespace Data.ValueObject
{
    [Serializable]
    public class SpawnInfoData
    {
        public EnemyController EnemyPrefab;
        public float MinSpawnTime = 3f;
        public float MaxSpawnTime = 7f;
         

        public EnemyController enemyPrefab => EnemyPrefab;
        public float RandomSpawnMaxTime => Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}