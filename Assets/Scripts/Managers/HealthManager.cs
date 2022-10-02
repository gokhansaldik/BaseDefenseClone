using System;
using Data.UnityObject;
using Interface;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour, IHealth
    {
        [SerializeField] private CD_Health healthInfo;
        private int _currentHealth;
        public bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _currentHealth = healthInfo.HealthData.maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if(IsDead) return;
            _currentHealth -= damage;
        }
    }
}