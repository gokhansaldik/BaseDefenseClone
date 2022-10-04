using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace er
{
    public class AiBase : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent aiNavmesh;
        public List<Collectable> MoneyList;
        //public List<Collectable> AmmoList;
        public void CollectMoney()
        {
            if (MoneyList != null)
            {
                aiNavmesh.SetDestination(MoneyList[0].transform.position);
            }
            
        }

        // public void CollectAmmo()
        // {
        //     if (AmmoList != null)
        //     {
        //         aiNavmesh.SetDestination(AmmoList[0].transform.position);
        //     }
        // }
        
        
    }
}