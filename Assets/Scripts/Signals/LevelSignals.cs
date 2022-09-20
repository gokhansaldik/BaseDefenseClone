using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public Func<int> onGetLevelID = delegate { return 0;};
    }
}