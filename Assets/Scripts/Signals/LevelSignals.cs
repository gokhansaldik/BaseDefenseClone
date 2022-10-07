using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        #region Level Initialize Signals

        public UnityAction onLevelInitialize = delegate { };

        #endregion

        #region Level Failed Signals

        public UnityAction onLevelFailed = delegate { };

        #endregion

        #region Get LevelId Signals

        public Func<int> onGetLevelID = delegate { return 0; };

        #endregion
    }
}