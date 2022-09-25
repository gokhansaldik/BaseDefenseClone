using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AmmoAreaManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleGameSignals.Instance.onGetAmmo += OnGetAmmo;
        }

        private void UnSubscribeEvent()
        {
            IdleGameSignals.Instance.onGetAmmo -= OnGetAmmo;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        private GameObject OnGetAmmo()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Bullet);
            if (obj == null)
                return null;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            return obj;
        }

    }
}
