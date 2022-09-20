using System;
using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public Func<ScoreDataParams> onScoreData = delegate { return default; };
        public UnityAction<PayType, int> onSetScore = delegate { };
        public UnityAction onSetScoreToUI = delegate { };
    }
}