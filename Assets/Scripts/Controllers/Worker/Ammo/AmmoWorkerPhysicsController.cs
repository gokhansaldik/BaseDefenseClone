using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Worker.Ammo
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {
        private int _timer;
        [SerializeField] private AmmoWorkerManager ammoWorkerManager;
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
