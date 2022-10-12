using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Worker.Ammo
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AmmoWorkerManager ammoWorkerManager;

        #endregion

        #region Private Variables

        private int _timer;

        #endregion

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                if (_timer >= 10)
                {
                    ammoWorkerManager.AddStack(IdleGameSignals.Instance.onGetAmmo());
                    _timer = _timer * 60 / 100;
                }
                else
                {
                    _timer++;
                }
            }
        }
    }
}