using Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Enemy
{
    public class EnemyMovementController : IMover
    {
        #region Self Variables

        #region Private Variables

        private NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public EnemyMovementController(IEnemyController enemyController)
        {
            _navMeshAgent = enemyController.transform.GetComponent<NavMeshAgent>();
        }

        public void MoveAction(Vector3 direction, float moveSpeed)
        {
            _navMeshAgent.SetDestination(direction);
        }
    }
}