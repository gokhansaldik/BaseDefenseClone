using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        #region Get Pool Signals

        public Func<PoolType, GameObject> onGetPoolObject = delegate { return null; };

        #endregion

        #region Set Pool Signals

        public UnityAction<GameObject, PoolType> onSendPool = delegate { };

        #endregion
    }
}