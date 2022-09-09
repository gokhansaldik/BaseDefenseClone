using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyTypeData
    {
        public EnemyType EnemyType;
        public int Health;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ChaseSpeed;
    }
}