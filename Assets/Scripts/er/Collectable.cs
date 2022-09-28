using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace er
{
    public class Collectable : MonoBehaviour
    {
        private AiBase Worker;
        // Money uzerinde olarak kullaniliyor
        [SerializeField] private AiManager aiManager;
        private void OnEnable()
        {
            if (aiManager.MoneyWorker != null)
            { 
                Worker = aiManager.MoneyWorker[Random.Range(0,aiManager.MoneyWorker.Count)];
                Worker.MoneyList.Add(this);
                Worker.CollectMoney();
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoneyWorker"))
            {
                transform.parent = other.transform;
                Worker.MoneyList.Remove(this);
                if (Worker.MoneyList != null)
                {
                    Worker.CollectMoney();
                }
                
            }
        }
    }
}
