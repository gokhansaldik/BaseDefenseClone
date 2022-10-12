using System.Collections.Generic;
using Controllers.Enemy;
using Extentions;
using Signals;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        #region Self Variables

        #region Public Variables

        public bool CanSpawn => maxCountOnGame > enemies.Count;

        #endregion

        #region Serialized Variables

        [SerializeField] private int maxCountOnGame = 40;
        [SerializeField] private HealthManager healthManager;
        public List<EnemyController> enemies;

        #endregion

        #endregion

        private void Awake()
        {
            enemies = new List<EnemyController>();
        }

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

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnPlayerDamage() => healthManager.TakeDamage(2);
        public void AddEnemyController(EnemyController enemyController)
        {
            enemyController.transform.parent = transform;
            enemies.Add(enemyController);
        }
    }
}