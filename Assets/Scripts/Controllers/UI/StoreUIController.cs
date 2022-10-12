using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Controllers.UI
{
    public class StoreUIController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;

        #endregion

        #endregion

        public void OpenStoreMenu(UIPanels storeMenu)
        {
            panels[(int)storeMenu].GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
            panels[(int)storeMenu].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public void CloseStoreMenu(UIPanels storeMenu)
        {
            panels[(int)storeMenu].GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
            panels[(int)storeMenu].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}