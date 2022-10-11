
using System.Collections;
using Controllers.Enemy;
using Data.UnityObject;
using DG.Tweening;
using Enums;
using Interface;
using System;
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
        public GameObject money;
        #endregion

        #region Serialized Variables
        
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyAnimationController enemyAnimationController;

        //[SerializeField] private EnemyController enemyController;

        #endregion
        #endregion
        private void Awake()
        {
            CurrentHealth = HealthInfo.HealthData.maxHealth;
        }
        
        public void EnemyAnim()
        {
            if (IsDead)
            {
                enemyAnimationController.Playanim(EnemyAnimationStates.Dead);
                //EnemyDestroy();
                //Destroy(gameObject);
                //enemyController.OnEnemyToMoney();
                
                Instantiate(money, new Vector3(transform.position.x,0,transform.position.z), transform.rotation);
                //InstantiateMoney();
                
                //gameObject.SetActive(false);
                //EnemySignals.Instance.onEnemyToMoney?.Invoke();
            }
        }
        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                playerManager.PlayerDead = true;
                playerManager.ChangePlayerAnimation(PlayerAnimationStates.Death);
               // StartCoroutine(PlayerRespawn());
               StartCoroutine(playerManager.PlayerRespawn());
               
            }
            CurrentHealth -= damage;
            HealthImage.fillAmount = Convert.ToSingle(CurrentHealth) / Convert.ToSingle(HealthInfo.HealthData.maxHealth);
        }

      
        // private IEnumerator PlayerRespawn()
        // {
        //     yield return new WaitForSeconds(1.5f);
        //     playerManager.PlayerDead = false;
        //     //PlayerSignals.Instance.onPlayerSpawned?.Invoke();
        //    // SetHealth();
        //     transform.parent.position = new Vector3(0, 0.4f, 0);
        //     playerManager.ChangePlayerAnimation(PlayerAnimationStates.Idle);
        // }
        private IEnumerator EnemyDestroy()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }

        private void InstantiateMoney()
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(money, new Vector3(transform.position.x,0,transform.position.z), transform.rotation);
                transform.DOJump(new Vector3(transform.position.x, 1, transform.position.z), 10, 1, 0.5f);
                
            }
        }
    }
}