using System;
using Class;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Interface;
using Managers;
using StateMachine;
using StateMachine.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IEntityController
    {
        [SerializeField] private Transform playerPrefab;
       // private IMover _mover;
        public GameObject EnemyTarget;
        private CharacterAnimation _animation;
        private NavMeshAgent _navMeshAgent;
        private StateMachinee _stateMachine;
        private bool _canAttack;
        public IMover Mover { get; private set; }

        public bool CanAttack => Vector3.Distance(playerPrefab.position, this.transform.position) <=
                                 _navMeshAgent.stoppingDistance &&
                                 _navMeshAgent.velocity == Vector3.zero;

        private void Awake()
        {
            Mover = new EnemyMovementController(this);
            _animation = new CharacterAnimation(this);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            playerPrefab = FindObjectOfType<PlayerManager>().transform;
            _stateMachine = new StateMachinee();
        }

        private void Start()
        {
            //playerPrefab = FindObjectOfType<PlayerManager>().transform;
            ChaseState chaseState = new ChaseState(this,playerPrefab);
            AttackState attackState = new AttackState();

            DeadState deadState = new DeadState();
            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            //_stateMachine.AddState(deadState,()=>_healt.IsDead);
            _stateMachine.SetState(chaseState);
        }

        private void Update()
        {
            // if (Vector3.Distance(playerPrefab.position, transform.position) < 2)
            // {
            //     Mover.MoveAction(playerPrefab.transform.position, 10f);
            // }
            // else
            // {
            //     Mover.MoveAction(EnemyTarget.transform.position, 10f);
            // }
            if (Vector3.Distance(playerPrefab.position, transform.position) < 2)
            {
                _stateMachine.Tick();
            }
            else
            {
                Mover.MoveAction(EnemyTarget.transform.position, 10f);
            }

            //_stateMachine.Tick();
        }

        private void LateUpdate()
        {
            _animation.MoveAnimation(0f);

            //_animation.MoveAnimation(_navMeshAgent.velocity.magnitude);
        }

        private void Condition()
        {
        }
    }
}