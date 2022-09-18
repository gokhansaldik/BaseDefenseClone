
using Abstract;
using AiBrains;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains
{
    public class Search :IState
    {
        private  Animator _animator;

        private  NavMeshAgent _navMeshAgent;

        private  EnemyBrain _enemyBrain;

        private Transform _spawnPoint;

        private bool _HasTarget=false;

        public Search(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, Transform spawnPoint)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _spawnPoint = spawnPoint;
        }

        public  void Enter()
        {
            _enemyBrain.enabled = true;
            GetRandomPointBakedSurface();
            
        }


        private void GetRandomPointBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * range;
                    Vector3 randomPosition = new Vector3(randomPoint.x, 0, center.z);
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }
            Vector3 point;
            if (!RandomPoint(_spawnPoint.position, 20, out point)) return;
            _navMeshAgent.Warp(point);
        }


        public void Exit() { }

        public void Tick() { }

    }
}