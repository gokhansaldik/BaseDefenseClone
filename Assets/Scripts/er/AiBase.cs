using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace er
{
    public class AiBase : MonoBehaviour
    {
        [SerializeField] protected Animator moneyWorkerAnimator;
        [SerializeField] protected NavMeshAgent aiNavmesh;

        public List<Collectable> MoneyList;
        
        public List<Collectable> AmmoList;

        [SerializeField] protected Transform BaseInTransform;
        [SerializeField] protected Transform BaseTurretTransform;

        public int MoneyWorkerCollectLimit = 10;
        public int AmmoWorkerCollectLimit = 10;

        public List<Collectable> CollectedMoneyList;
        public List<Collectable> CollectedAmmoList;
        
        
        
        //[SerializeField] private Transform InBaseTransform;
        public virtual void Collect()
        { 
            
        }

      

        // private void Start()
        // {
        //     GoToBase();
        // }

        protected void Update()
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
       
        public void GoToTarget(Transform target)
        {
            aiNavmesh.SetDestination(target.position);
        }
        
        
    }
}