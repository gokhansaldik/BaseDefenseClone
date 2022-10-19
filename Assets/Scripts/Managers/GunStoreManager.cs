using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GunStoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        
        [SerializeField] private UIPanels releatedPanel;

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
        private ItemPricesData GetData()
        {
            return Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_GunPrices").Data;
        }
        public void CloseBtn() => UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
       
    }
}