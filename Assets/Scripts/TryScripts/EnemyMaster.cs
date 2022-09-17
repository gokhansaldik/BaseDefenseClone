using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace TryScripts
{
    public class EnemyMaster : MonoBehaviour
    {
        public GameObject Player;
        public float Distance;
        public bool isAngered;
        public NavMeshAgent Agent;

        private void Start()
        {
            Player = FindObjectOfType<PlayerManager>().gameObject;
        }

        private void Update()
        {
            Distance = Vector3.Distance(Player.transform.position, this.transform.position);
            if (Distance <= 5)
            {
                isAngered = true;
            }

            if (Distance > 5f)
            {
                isAngered = false;
            }

            if (isAngered)
            {
                Agent.isStopped = false;
                Agent.SetDestination(Player.transform.position);
            }

            if (!isAngered)
            {
                Agent.isStopped = true;
            }
        }
    }
}
