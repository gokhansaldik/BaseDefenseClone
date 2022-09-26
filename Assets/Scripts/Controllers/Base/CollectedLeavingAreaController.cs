using Controllers.Player;
using UnityEngine;

namespace Controllers.Base
{
    public class CollectedLeavingAreaController : MonoBehaviour
    {
        
       [SerializeField] private PlayerStackController _playerStackController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectedMoney"))
            {
                //TODO : money 2. girme bugu
                _playerStackController.MoneyLeaving(other.gameObject);
                
            }
            else if (other.CompareTag("BulletBox"))
            {
                _playerStackController.BulletBoxLeaving(other.gameObject);
                Debug.Log("bullet trigger");
            }
        }

        
    }
}