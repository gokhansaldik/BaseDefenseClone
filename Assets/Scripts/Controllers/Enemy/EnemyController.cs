using Class;
using Interface;
using Managers;
using StateMachine;
using StateMachine.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IEnemyController
    {
        #region Self Variables

        #region Public Variables

        public GameObject EnemyTarget;
        public EnemyDeadController Dead { get; private set; }

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform playerPrefab;
        [SerializeField] private HealthManager healthManager;

        #endregion


        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private StateMachinee _stateMachine;
        private bool _isMoneyInstantiated = false;

        #endregion

        #endregion


        public IMover Mover { get; private set; }
        public CharacterAnimation Animation { get; private set; }

        public bool CanAttack => Vector3.Distance(playerPrefab.position, transform.position) <=
                                 _navMeshAgent.stoppingDistance &&
                                 _navMeshAgent.velocity == Vector3.zero;

        private void Awake()
        {
            Mover = new EnemyMovementController(this);
            Animation = new CharacterAnimation(this);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            playerPrefab = FindObjectOfType<PlayerManager>().transform;
            _stateMachine = new StateMachinee();
            Dead = GetComponent<EnemyDeadController>();
        }

        private void Start()
        {
            var chaseState = new ChaseState(this, playerPrefab);
            var attackState = new AttackState(this);
            var deadState = new DeadState(this);
            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            _stateMachine.SetState(chaseState);
        }

        private void Update()
        {
            if (Vector3.Distance(playerPrefab.position, transform.position) < 2 && healthManager.IsDead == false)
                _stateMachine.Tick();
            else if (Vector3.Distance(playerPrefab.position, transform.position) > 2 && healthManager.IsDead == false)
                Mover.MoveAction(EnemyTarget.transform.position, 10f);
            else
                Mover.MoveAction(gameObject.transform.position, 0);
            
            //gameObject.SetActive(false);  //TODO: setActive false veya destroy bug
        }

        private void LateUpdate()
        {
            Animation.MoveAnimation(0f);
        }
    }
}