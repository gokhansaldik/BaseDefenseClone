using Managers;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysics : MonoBehaviour
    {
        [SerializeField]
        private GameObject collisionColliderObj;
        private Transform _detectedPlayer;
        private Transform _detectedMine;

        private EnemyAIBrain _enemyAIBrain;
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                //sinyalle �akmay� dene
                _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
            }

            if (other.CompareTag("Bullet"))
            {
                _enemyAIBrain._health = 0;
                collisionColliderObj.SetActive(false);
            }

            /*if (other.GetComponent<Mine>())
    {
        _detectedMine = other.GetComponent<Mine>();
    }*/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
            }

            /*if (other.GetComponent<Mine>())
    {

    }*/
        }
    }
}
