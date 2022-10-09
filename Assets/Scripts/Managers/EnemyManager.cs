using System.Collections.Generic;
using Controllers.Enemy;
using Extentions;
using Signals;
using TMPro.EditorUtilities;
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
        [SerializeField] private List<EnemyController> enemies;
        
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
        private void OnPlayerDamage()
        {
            // if (healthManager.CurrentHealth >0)
            // { 
                healthManager.TakeDamage(2);
            //}
               
            
           
        }
       
        #endregion
        public void AddEnemyController(EnemyController enemyController)
        {
            enemyController.transform.parent = this.transform;
            enemies.Add(enemyController);
        }

        
    }
}