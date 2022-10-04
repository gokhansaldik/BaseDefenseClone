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
      //  [SerializeField] private GameObject ammoArea;
        private void OnEnable()
        {
            if (aiManager.MoneyWorker != null)
            { 
                Worker = aiManager.MoneyWorker[Random.Range(0,aiManager.MoneyWorker.Count)];
                Worker.MoneyList.Add(this);
                Worker.CollectMoney();
            }

            // if (aiManager.AmmoWorker != null)
            // {
            //     Worker = aiManager.AmmoWorker[aiManager.AmmoWorker.Count];
            //     Worker.AmmoList.Add(this);
            //     Worker.CollectAmmo();
            // }
            
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
            // else if (other.CompareTag("AmmoWorker"))
            // {
            //     transform.parent = other.transform;
            //     Worker.AmmoList.Remove(this);
            //     if (Worker.AmmoList != null)
            //     {
            //         Worker.CollectAmmo();
            //     }
            // }
        }
    }
}
