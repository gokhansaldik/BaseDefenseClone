using Enums;
using UnityEngine;

namespace Abstract
{
    public abstract class AiEnemy
    {
        public EnemyType EnemyType;
        public int Health;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public Vector3 ScaleSize;
        public Color BodyColor;

        protected AiEnemy(EnemyType enemyType, int health, int damage, float attackRange, float attackSpeed,
            Vector3 scaleSize, Color bodyColor)
        {
            EnemyType = enemyType;
            Health = health;
            Damage = damage;
            AttackRange = attackRange;
            AttackSpeed = attackSpeed;
            ScaleSize = scaleSize;
            BodyColor = bodyColor;
        }
    }
}