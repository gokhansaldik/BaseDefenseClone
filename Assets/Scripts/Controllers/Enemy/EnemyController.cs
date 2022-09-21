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
        public GameObject EnemyTarget;
        

        private void Awake()
        {
            _mover = new EnemyMovementController(this);
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

    }
}