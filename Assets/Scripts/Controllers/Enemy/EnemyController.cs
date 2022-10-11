using System.Collections;
using Class;
using Enums;
using Interface;
using Managers;
using Signals;
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
      
        #endregion


        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private StateMachinee _stateMachine;
        private bool _isMoneyInstantiated = false;

        #endregion
        #endregion
      
       
        public IMover Mover { get; private set; }
        public CharacterAnimation Animation { get; private set; }
        public bool CanAttack => Vector3.Distance(playerPrefab.position, this.transform.position) <=
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
            ChaseState chaseState = new ChaseState(this, playerPrefab);
            AttackState attackState = new AttackState(this);
            DeadState deadState = new DeadState(this);
            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            
            
            _stateMachine.SetState(chaseState);
        }

        private void Update()
        {
            if (Vector3.Distance(playerPrefab.position, transform.position) < 2)
            {
                _stateMachine.Tick();
            }
            else
            {
                Mover.MoveAction(EnemyTarget.transform.position, 10f);
            }
        }

        private void LateUpdate()
        {
            Animation.MoveAnimation(0f);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
          
        }
        private void UnsubscribeEvents()
        {
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        // private IEnumerator DeactivateEnemy()
        // {
        //     InstantiateMoneys();
        //     yield return new WaitForSeconds(2f);
        //     EnemySignals.Instance.onEnemyToMoney?.Invoke();
        //     gameObject.SetActive(false);
        // }
        // public void InstantiateMoneys()
        // {
        //     if (!_isMoneyInstantiated)
        //     {
        //         for (int i = 0; i < 3; i++)
        //         {
        //             //Instantiate(moneyPrefab, transform.position, transform.rotation);
        //             GameObject tmp = PoolSignals.Instance.onGetPoolObject(PoolType.Money);
        //             if (tmp == null)
        //             {
        //                 tmp = Instantiate(money, transform.position, transform.rotation);
        //             }
        //             tmp.transform.position = transform.position;
        //             tmp.transform.rotation = transform.rotation;
        //             tmp.SetActive(true);
        //
        //         }
        //         _isMoneyInstantiated = true;
        //        
        //     }
        // }
       

        // public void OnEnemyToMoney()
        // {
        //     this.gameObject.SetActive(false);
        //     money.SetActive(true);
        // }
    }
}