using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class HealthBarController : MonoBehaviour
    {
       public Image healthImage;
       public HealthManager _healthManager;
       private void OnEnable()
       {
           _healthManager = GetComponent<HealthManager>();
           //_healthManager.OnTakeHit += OnTakeHit;
       }
       
       // private void OnDisable()
       // {
       //     HealthManager healthManager = GetComponentInParent<HealthManager>();
       //     healthManager.onTakeHit -= OnTakeHit;
       // }
       private void OnTakeHit(int currentHealth, int maxHealth)
       {
           //Convert.ToSingle(currentHealth) / Convert.ToString(maxHealt);
           healthImage.fillAmount = Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
           
       }
    }
}
