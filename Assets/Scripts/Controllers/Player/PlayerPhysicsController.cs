using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion
        #region Private Variables

        private int _timer;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectablePlayer"))
            {
                StackSignals.Instance.onAddInStack?.Invoke(other.gameObject.transform.parent.gameObject);
                other.gameObject.tag = "Collected";
            }

            else if (other.CompareTag("CollectableMoney"))
            {
                playerManager.AddStack(other.gameObject);
                other.gameObject.tag = "CollectedMoney";
            }
          
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                //TODO : player havaya kalkiyor .
                if (_timer >= 10)
                {
                    playerManager.AddStack(IdleGameSignals.Instance.onGetAmmo());
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