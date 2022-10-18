using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onAddInStack = delegate { }; 
        public UnityAction onCollectablePlayerMiner = delegate { }; 
        public UnityAction onCollectablePlayerTaken = delegate { }; 
        public UnityAction onCollectableUpSpeed = delegate { }; 
        public UnityAction onCollectableUpDown = delegate { };
    }
}