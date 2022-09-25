using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction onBaseAreaBuyedItem = delegate { };
        public Func<GameObject> onGetMineTarget = delegate { return default; };
        public Func<GameObject> onGetMineStackTarget = delegate { return default; };
        public UnityAction<GameObject> onAddDiamondStack = delegate { };
        
        public Func<GameObject> onGetAmmo = delegate { return default; };
        
    }
}