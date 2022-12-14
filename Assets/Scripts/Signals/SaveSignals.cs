using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        #region Save Signals

        public UnityAction onLevelSave = delegate { };
        public Func<int> onSaveLevelData = delegate { return 0; };
        public UnityAction onScoreSave = delegate { };
        public Func<ScoreDataParams> onSaveScoreData = delegate { return default; };
        public UnityAction onAreaDataSave = delegate { };
        public Func<AreaDataParams> onSaveAreaData = delegate { return default; };

        #endregion

        #region Load Signals

        public Func<int> onLoadCurrentLevel = delegate { return 0; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default; };
        public Func<AreaDataParams> onLoadAreaData = delegate { return default; };

        #endregion
    }
}