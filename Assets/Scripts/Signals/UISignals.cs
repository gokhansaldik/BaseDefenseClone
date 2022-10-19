using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        #region Panel Signals

        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;

        #endregion

        #region Store Panel Signals

        public UnityAction<UIPanels> onOpenStorePanel;
        public UnityAction<UIPanels> onCloseStorePanel;

        #endregion

        #region Gun Signals

        //public UnityAction<List<int>> onInitializeGunLevels;
        //public UnityAction<List<int>> onChangeGunLevels;

        #endregion

        #region Worker Signals

        public UnityAction<List<int>> onInitializeWorkerUpgrades = delegate { };
        
        #endregion
    }
}