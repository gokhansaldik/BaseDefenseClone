using System;
using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction onBuyArea = delegate { };
        public UnityAction<int> onMoneyDown = delegate { };
        public UnityAction<int> onDiamondDown = delegate { };

        
        public UnityAction<int> onAddMoney = delegate { };
        public UnityAction<int> onAddDiamond = delegate { };
        
        public Func<ScoreDataParams> onScoreData = delegate { return default;};
        public UnityAction<PayType,int> onSetScore =delegate {  };
        public UnityAction onSetScoreToUI = delegate { };
    }
}