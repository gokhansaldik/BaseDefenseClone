using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using Enums;
using TMPro;


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
        private ItemPricesData GetData() =>
            Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_WorkerUpgradePrices").Data;

        private void Start()
        {
            UpdateTexts();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onInitializeWorkerUpgrades += OnGetItemLevels;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onInitializeWorkerUpgrades -= OnGetItemLevels;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnGetItemLevels(List<int> levels)
        {
            if (levels.Count.Equals(0))
            {
                levels = new List<int>() { 2, 0 };
            }
            itemLevels = levels;
        }
        public void UpdateTexts()
        {
            for (int i = 0; i < itemLevels.Count; i++) 
            {
                levelTxt[i].text = "LEVEL " + (itemLevels[i] + 1).ToString();
                upgradeTxt[i].text = _data.ItemPrices[i].Prices[itemLevels[i]].ToString();
            }
        }
        public void CloseBtn()
        {
            UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
        }
    }
}