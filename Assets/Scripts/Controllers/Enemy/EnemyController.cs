using System;
using Class;
using Enums;
using Interface;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IEntityController
    {
        
        [SerializeField] private Transform playerPrefab;
        private IMover _mover;
        public GameObject EnemyTarget;
        private CharacterAnimation _animation;
        //private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _mover = new EnemyMovementController(this);
            _animation = new CharacterAnimation(this);
            //_navMeshAgent = GetComponent<NavMeshAgent>();
            playerPrefab =FindObjectOfType<PlayerManager>().transform;
        }

        private void Update()
        {
            if (Vector3.Distance(playerPrefab.position,transform.position)<2)
            {
                _mover.MoveAction(playerPrefab.transform.position, 10f);
            }
            else
            {
                _mover.MoveAction(EnemyTarget.transform.position,10f);
            }
            
           
            
        }

        private void LateUpdate()
        {
            _animation.MoveAnimation(0f);
            //_animation.MoveAnimation(_navMeshAgent.velocity.magnitude);
        }
    }
}