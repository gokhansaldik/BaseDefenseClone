using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class WorkerStorePanelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<TextMeshProUGUI> upgradeTxt;
        [SerializeField] private List<int> itemLevels;
        [SerializeField] private UIPanels releatedPanel;

        #endregion

        private ItemPricesData _data;

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }

        private ItemPricesData GetData()
        {
            return Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_WorkerUpgradePrices").Data;
        }

      

        public void CloseBtn()
        {
            UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
        }
    }
}