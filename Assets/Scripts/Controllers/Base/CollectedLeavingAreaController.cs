using Controllers.Player;
using UnityEngine;

namespace Controllers.Base
{
    public class CollectedLeavingAreaController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerStackController _playerStackController;

        #endregion

        #endregion

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
            }
        }
    }
}