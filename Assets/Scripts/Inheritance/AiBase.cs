using System.Collections.Generic;
using Controllers.Player;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance
{
    public class AiBase : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int AmmoWorkerCollectLimit = 10;
        public List<Collectable> AmmoList;
        public List<Collectable> CollectedAmmoList;

        public int MoneyWorkerCollectLimit = 10;
        public List<Collectable> MoneyList;
        public List<Collectable> CollectedMoneyList;

        #endregion

        #region Serialized Variables

        [SerializeField] protected Animator moneyWorkerAnimator;
        [SerializeField] protected NavMeshAgent aiNavmesh;
        [SerializeField] protected Transform BaseTurretTransform;
        [SerializeField] protected Transform BaseInTransform;

        #endregion

        #endregion

        public virtual void Collect()
        {
        }

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
            moneyWorkerAnimator.SetFloat("Speed", Speed, 2f, Time.deltaTime);
        }

        public void GoToTarget(Transform target)
        {
            aiNavmesh.SetDestination(target.position);
        }
    }
}