using Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Enemy
{
    public class EnemyMovementController : IMover
    {
        private NavMeshAgent _navMeshAgent;

        public EnemyMovementController(IEntityController entityController)
        {
            _navMeshAgent = entityController.transform.GetComponent<NavMeshAgent>();
        }

        public void MoveAction(Vector3 direction, float moveSpeed)
        {
           // _navMeshAgent.speed = moveSpeed;
            _navMeshAgent.SetDestination(direction);
        }
    }
}
