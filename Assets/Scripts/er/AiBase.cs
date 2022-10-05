using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace er
{
    public class AiBase : MonoBehaviour
    {
        [SerializeField] private Animator moneyWorkerAnimator;
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

        private void Update()
        {
            if (MoneyList.Count > 0)
            {
                MoneyWorkerMoveAnimation(2f);
            }
            else
            {
                MoneyWorkerMoveAnimation(0f);
            }
            
        }

        public void MoneyWorkerMoveAnimation(float Speed)
        {
            if (moneyWorkerAnimator.GetFloat("Speed") == Speed) ;
             moneyWorkerAnimator.SetFloat("Speed",Speed,2f,Time.deltaTime);
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