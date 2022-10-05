using System.Collections.Generic;
using Controllers.Enemy;
using Enums;
using Extentions;
using Signals;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        [SerializeField] private int maxCountOnGame = 40;
        [SerializeField] private List<EnemyController> enemies;
        [SerializeField] private HealthManager _healthManager;
        
        public bool CanSpawn => maxCountOnGame > enemies.Count;
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onPlayerDamage += OnPlayerDamage;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onPlayerDamage -= OnPlayerDamage;
        }

        private void OnPlayerDamage()
        {
            // Signals ile damage islemi yapiliyor.
            _healthManager.TakeDamage(2);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void Awake()
        {
            enemies = new List<EnemyController>();
        }

        public void AddEnemyController(EnemyController enemyController)
        {
            enemyController.transform.parent = this.transform;
            enemies.Add(enemyController);
        }

        public void RemoveEnemyController(EnemyController enemyController)
        {
            enemies.Remove(enemyController);
        }

        
    }
}