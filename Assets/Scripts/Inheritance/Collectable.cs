using Controllers.Player;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inheritance
{
    public class Collectable : MonoBehaviour
    {
        private AiBase _worker;
        // Money uzerinde olarak kullaniliyor
        [SerializeField] private AiManager aiManager;
        [SerializeField] private PlayerStackController playerStackController;
        private void OnEnable()
        {
            if (CompareTag("BulletBox"))
            {
                // if (aiManager.AmmoWorker != null)
                // { 
                //     _worker = aiManager.AmmoWorker[Random.Range(0,aiManager.AmmoWorker.Count)];
                //     _worker.AmmoList.Add(this);
                //     _worker.Collect();
                // }
            }
            if (CompareTag("CollectableMoney"))
            {
                if (aiManager.MoneyWorker != null)
                { 
                    _worker = aiManager.MoneyWorker[Random.Range(0,aiManager.MoneyWorker.Count)];
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
