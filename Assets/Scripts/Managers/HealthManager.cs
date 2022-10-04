using System;
using System.Reflection;
using Controllers.UI;
using Data.UnityObject;
using Enums;
using Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class HealthManager : MonoBehaviour, IHealth
    {
        public Image healthImage;
        
        [SerializeField] public CD_Health healthInfo;
        public int CurrentHealth;
        public bool IsDead => CurrentHealth <= 0;

        [SerializeField]private PlayerManager _playerManager;
       // public event UnityAction<int, int> OnTakeHit;
        private void Awake()
        {
            CurrentHealth = healthInfo.HealthData.maxHealth;
          // healthImage = GameObject.FindGameObjectWithTag("Health");
          //healthImage = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();

        }

      


        public void TakeDamage(int damage)
        {
            // Player can dusuren fonksiyon.
            if (IsDead)
            {
                _playerManager.ChangePlayerAnimation(PlayerAnimationStates.Death);
            }
            CurrentHealth -= damage;
            //OnTakeHit?.Invoke(_currentHealth,healthInfo.HealthData.maxHealth);
            healthImage.fillAmount = Convert.ToSingle(CurrentHealth) / Convert.ToSingle(healthInfo.HealthData.maxHealth);
           
        }
        private void OnTakeHit(int currentHealth, int maxHealth)
        {
            //Convert.ToSingle(currentHealth) / Convert.ToString(maxHealt);
            healthImage.fillAmount = Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
           
        }

       
    }
}