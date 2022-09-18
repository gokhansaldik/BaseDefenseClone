
using Abstract;
using AiBrains;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains
{
    public class Death : IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private Transform _spawnPoint;

        private float _movementSpeed;

        private Transform _turretTransform;

        private Transform _playerTransform;

        private float _atackRange;

        private float _damage;

        private float _healt;

        private float _playerDamage;



        public  void Enter()
        {
            _navMeshAgent.enabled = false;
        }

        public void Exit()
        {
          
        }

        public void Tick()
        {
          
        }
    }
}