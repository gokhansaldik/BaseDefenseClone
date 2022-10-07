using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using Enums;
using TMPro;


namespace Managers
{
    public class GunStoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<TextMeshProUGUI> upgradeTxt;
        [SerializeField] private List<int> itemLevels;

        #endregion
        
        #region Private Variables

        private ItemPricesData _data;

        #endregion
        #endregion
        
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _data = GetData();
        }
        private ItemPricesData GetData() => Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_GunPrices").Data;
       
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            UISignals.Instance.onInitializeGunLevels += OnGetItemLevels;
        }
        private void UnsubscribeEvents()
        {
            UISignals.Instance.onInitializeGunLevels -= OnGetItemLevels;
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
                levels = new List<int>() { 1, 0, 0, 0, 0, 0 };
            }
            itemLevels = levels;
        }
        public void CloseBtn()
        {
            UISignals.Instance.onCloseStorePanel?.Invoke(UIPanels.GunStorePanel);
        }
    }
}