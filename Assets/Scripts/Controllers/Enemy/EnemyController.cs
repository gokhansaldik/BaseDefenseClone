using Enums;
using Interface;
using Managers;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IEntityController
    {
        
        [SerializeField] private Transform playerPrefab;
        private IMover _mover;
        

        private void Awake()
        {
            _mover = new EnemyMovementController(this);
            playerPrefab =FindObjectOfType<PlayerManager>().transform;
        }

        private void Update()
        {
            _mover.MoveAction(playerPrefab.transform.position, 10f);
            
        }

    }
}