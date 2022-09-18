using System;
using ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction onAreaCostDown = delegate { };
        public UnityAction onAreaComplete = delegate { };
        public UnityAction onCityComplete = delegate { };
        public UnityAction onRefreshAreaData = delegate { };
        public UnityAction onPrepareAreaWithSave = delegate { };
        public UnityAction<GameObject> onCheckArea = delegate { };
        public UnityAction<int, AreaData> onSetAreaData = delegate { };
        public Func<int, AreaData> onGetAreaData = delegate { return default; };
        public UnityAction onBaseAreaBuyedItem = delegate {  };
        
        #region Mine Signals

        public Func<GameObject> onGetMineTarget= delegate { return default;};
        public Func<GameObject> onGetMineStackTarget= delegate { return default;};
        public UnityAction<GameObject> onAddDiamondStack= delegate { };   
    

        #endregion
    }
}