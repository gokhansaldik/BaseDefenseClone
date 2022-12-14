using Controllers.Player;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inheritance
{
    public class Collectable : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AiManager aiManager;
        [SerializeField] private PlayerStackController playerStackController;

        #endregion

        #region Private Variables

        private AiBase _worker;

        #endregion

        #endregion

        private void OnEnable()
        {
            if (CompareTag("BulletBox"))
            {
            }

            if (CompareTag("CollectableMoney"))
            {
                if (aiManager.MoneyWorker != null)
                {
                    _worker = aiManager.MoneyWorker[Random.Range(0, aiManager.MoneyWorker.Count)];
                    _worker.MoneyList.Add(this);
                    _worker.Collect();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoneyWorker"))
            {
                transform.parent = other.transform;
                _worker.MoneyList.Remove(this);
                _worker.CollectedMoneyList.Add(this);
                playerStackController.SetObjectPosition(gameObject);
                if (_worker.MoneyList != null)
                {
                    _worker.Collect();
                }
            }
        }
    }
}