using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onAddInStack = delegate { };
        public UnityAction<GameObject> onRemoveInStack = delegate { };
        public UnityAction<GameObject> onGetStackList = delegate { };
    }
}