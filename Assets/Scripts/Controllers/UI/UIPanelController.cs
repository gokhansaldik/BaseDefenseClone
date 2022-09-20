using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers.UI
{
    public class UIPanelController
    {
        public void OpenUIPanel(UIPanels panelParam, List<GameObject> panels)
        {
            panels[(int)panelParam].SetActive(true);
        }

        public void CloseUIPanel(UIPanels panelParam, List<GameObject> panels)
        {
            panels[(int)panelParam].SetActive(false);
        }
    }
}