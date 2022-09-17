using UnityEngine;
using UnityEngine.AI;
using Interface;
namespace StateMachine
{
    public class AttackState: IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;

        private bool _inAttack;
        public bool InPlayerAttackRange() => _inAttack;
        public AttackState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain, float attackRange)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = attackRange;
        }
        public void OnEnter()
        {
            if (_enemyAIBrain.PlayerTarget)
            {
                _inAttack = true;
                _animator.SetTrigger("Attack");
            }
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            if (_enemyAIBrain.PlayerTarget)
            {
                _navMeshAgent.destination =_enemyAIBrain.PlayerTarget.transform.position;
                Debug.Log("Dist: " + _navMeshAgent.remainingDistance);
                CheckDistanceAttack();
            }
        }
        private void CheckDistanceAttack()
        {

            if (_navMeshAgent.remainingDistance > _attackRange)
                _inAttack = false;
        }
    }
}