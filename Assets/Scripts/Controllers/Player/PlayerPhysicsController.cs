using DG.Tweening;
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
                StackSignals.Instance.onCollectablePlayerTaken?.Invoke();
            }

            if (other.CompareTag("CollectableMoney"))
            {
                playerManager.AddStack(other.gameObject);
                other.gameObject.tag = "CollectedMoney";
            }

            if (other.CompareTag("BaseOutTrigger"))
            {
                playerManager.InBase = false;
                IdleGameSignals.Instance.onOpenPlayerHealthBar?.Invoke();
            }

            if (other.CompareTag("InBaseTrigger"))
            {
                playerManager.InBase = true;
                IdleGameSignals.Instance.onClosePlayerHealthBar?.Invoke();
            }

            if (other.CompareTag("Turret"))
            {
                var newparent = other.GetComponent<TurretManager>().PlayerHandle.transform;
                playerManager.transform.parent = newparent;
                playerManager.transform.DOLocalMove(new Vector3(0, playerManager.transform.localPosition.y, 0), .5f);
                playerManager.transform.DOLocalRotate(Vector3.zero, 0.5f);
                IdleGameSignals.Instance.onPlayerInTurret.Invoke(other.gameObject);
            }
            else if (other.CompareTag("TurretStack"))
            {
                PlayerSignals.Instance.onPlayerReachTurretAmmoArea?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
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