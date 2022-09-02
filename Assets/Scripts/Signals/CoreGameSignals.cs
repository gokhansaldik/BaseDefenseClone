using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction<GameStatesType> onSetGameState = delegate { };
        public UnityAction<GameStatesType> onGetGameState = delegate { };

        public UnityAction onEnterFinish = delegate { };

        public UnityAction<Transform> onSetCameraTarget = delegate { };
    }
}