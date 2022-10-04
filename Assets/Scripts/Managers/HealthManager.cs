using System;
using System.Reflection;
using Controllers.UI;
using Data.UnityObject;
using Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class HealthManager : MonoBehaviour, IHealth
    {
        [SerializeField] private Image healthImage;
        
        [SerializeField] private CD_Health healthInfo;
        private int _currentHealth;
        public bool IsDead => _currentHealth <= 0;
       // public event UnityAction<int, int> OnTakeHit;
        private void Awake()
        {
            _currentHealth = healthInfo.HealthData.maxHealth;
          // healthImage = GameObject.FindGameObjectWithTag("Health");
          //healthImage = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();

        }
        

        public void TakeDamage(int damage)
        {
            // Player can dusuren fonksiyon.
            if(IsDead) return;
            _currentHealth -= damage;
            //OnTakeHit?.Invoke(_currentHealth,healthInfo.HealthData.maxHealth);
            healthImage.fillAmount = Convert.ToSingle(_currentHealth) / Convert.ToSingle(healthInfo.HealthData.maxHealth);
           
        }
        private void OnTakeHit(int currentHealth, int maxHealth)
        {
            //Convert.ToSingle(currentHealth) / Convert.ToString(maxHealt);
            healthImage.fillAmount = Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
           
        }
    }
}