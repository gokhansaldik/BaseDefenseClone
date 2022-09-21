using System.Collections.Generic;
using Controllers.Enemy;
using Extentions;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        [SerializeField] private int maxCountOnGame = 40;
        [SerializeField] private List<EnemyController> enemies;
        public bool CanSpawn => maxCountOnGame > enemies.Count;
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