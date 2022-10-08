using System;
using Controllers.Enemy;
using Data.UnityObject;
using Enums;
using Interface;
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

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyAnimationController enemyAnimationController;

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
            }
        }
        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                playerManager.ChangePlayerAnimation(PlayerAnimationStates.Death);
            }

            CurrentHealth -= damage;
            HealthImage.fillAmount = Convert.ToSingle(CurrentHealth) / Convert.ToSingle(HealthInfo.HealthData.maxHealth);
        }
    }
}