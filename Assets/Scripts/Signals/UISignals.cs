using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        public UnityAction<UIPanels> onOpenStorePanel;
        public UnityAction<UIPanels> onCloseStorePanel;
        public UnityAction<List<int>> onInitializeGunLevels;
        public UnityAction<List<int>> onChangeGunLevels;
    }
}