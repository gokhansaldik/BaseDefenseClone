using System;
using Controllers.Enemy;
using Controllers.Player;
using Data.UnityObject;
using Enums;
using Interface;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class HealthManager : MonoBehaviour, IHealth
    {
        #region Self Variables

        #region Public Variables

        public bool IsDead => CurrentHealth <= 0;
        public Image HealthImage;
        public int CurrentHealth;
        public CD_Health HealthInfo;
        public GameObject Money;

        #endregion

        #region Serialized Variables
        
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyAnimationController enemyAnimationController;
       //  private EnemyManager enemyManager;
       private  PlayerAimController playerAimController;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        private void Awake()
        {
            CurrentHealth = HealthInfo.HealthData.maxHealth;
            
        }


        private void Start()
        {
            playerAimController = FindObjectOfType<PlayerAimController>();
        }

        public void EnemyAnim()
        {
            if (IsDead)
            {
                enemyAnimationController.Playanim(EnemyAnimationStates.Dead);
                Instantiate(Money, new Vector3(transform.position.x, 0.073f, transform.position.z), transform.rotation);
               // enemyManager.enemies.Remove(gameObject.GetComponent<EnemyController>());
                //EnemySignals.Instance.onEnemyDie?.Invoke(enemyManager.transform);
                playerAimController.targetList.Remove(transform);
            }
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                playerManager.PlayerDead = true;
                playerManager.ChangePlayerAnimation(PlayerAnimationStates.Death);
                StartCoroutine(playerManager.PlayerRespawn());
            }

            CurrentHealth -= damage;
            HealthImage.fillAmount = Convert.ToSingle(CurrentHealth) / Convert.ToSingle(HealthInfo.HealthData.maxHealth);
        }
    }
}