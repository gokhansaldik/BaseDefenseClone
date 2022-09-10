using UnityEngine;
using UnityEngine.Events;
using Extentions;
using System.Collections.Generic;
using Enums;
using System;
using UnityEngine.UI;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onAddInStack = delegate { };
        public UnityAction<GameObject> onRemoveInStack = delegate { };
        public UnityAction<GameObject, Transform> onTransportInStack = delegate { };
        public UnityAction<GameObject> onGetStackList = delegate { };
        
        public UnityAction<GameObject> onAddStackMoney =  delegate { };
        public UnityAction<GameObject> onRemoveStackMoney =  delegate { };
        
        public UnityAction onStackTransferComplete = delegate { };

        public UnityAction<Transform> onCollectablesThrow = delegate { };
    }
}