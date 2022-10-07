using System;
using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        #region Get Score Signals

        public Func<ScoreDataParams> onScoreData = delegate { return default; };

        #endregion

        #region Set Score Signals

        public UnityAction onSetScoreToUI = delegate { };
        public UnityAction<PayType, int> onSetScore = delegate { };

        #endregion
    }
}