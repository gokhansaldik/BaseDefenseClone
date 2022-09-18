using System;
using Abstract;
using UnityEngine;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyTypeData : AiEnemy
    {
        // public EnemyType EnemyType;
        // public int Health;
        // public int Damage;
        // public float AttackRange;
        // public float AttackSpeed;
        
        public float MoveSpeed;
        public float ChaseSpeed;
        public float NavMeshRadius;
        public float NavMeshHeight;
        public EnemyTypeData(EnemyType enemyType, int health, int damage, float attackRange, float attackSpeed, Vector3 scaleSize, Color bodyColor) : base(enemyType, health, damage, attackRange, attackSpeed, scaleSize, bodyColor)
        {
        }
    }
}