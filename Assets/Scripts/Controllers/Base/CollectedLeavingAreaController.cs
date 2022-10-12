using Controllers.Player;
using UnityEngine;

namespace Controllers.Base
{
    public class CollectedLeavingAreaController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerStackController playerStackController;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectedMoney")) playerStackController.MoneyLeaving(other.gameObject);

            if (other.CompareTag("BulletBox")) playerStackController.BulletBoxLeaving(other.gameObject);
        }
    }
}