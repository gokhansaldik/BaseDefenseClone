using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.UI;
using Enums;
using Keys;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI gemText;
        [SerializeField]private StoreUIController storeUIController;
        [SerializeField] private GameObject loadingImage;
        #endregion

        #region Private Variables

        private UIPanelController _uiPanelController;
        private ScoreDataParams _scoreData;
       
        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            StartCoroutine(LoadImage());
        }
        private void GetReferences()
        {
            _uiPanelController = new UIPanelController();
            
        }
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            ScoreSignals.Instance.onSetScoreToUI += OnSetScoreText;
            UISignals.Instance.onOpenStorePanel += OnOpenStorePanel;
            UISignals.Instance.onCloseStorePanel += OnCloseStorePanel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            ScoreSignals.Instance.onSetScoreToUI -= OnSetScoreText;
            UISignals.Instance.onOpenStorePanel -= OnOpenStorePanel;
            UISignals.Instance.onCloseStorePanel -= OnCloseStorePanel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnOpenPanel(UIPanels panelParam)
        {
            _uiPanelController.OpenUIPanel(panelParam, panels);
        }
        private void OnClosePanel(UIPanels panelParam)
        {
            _uiPanelController.CloseUIPanel(panelParam, panels);
        }
        private void OnSetScoreText()
        {
            _scoreData = ScoreSignals.Instance.onScoreData();
            moneyText.text = _scoreData.MoneyScore.ToString();
            gemText.text = _scoreData.GemScore.ToString();
        }
        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.ScorePanel);
        }
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        private void OnOpenStorePanel(UIPanels panelParam)
        {
            storeUIController.OpenStoreMenu(panelParam);
        }
        private void OnCloseStorePanel(UIPanels panelParam)
        {
            storeUIController.CloseStoreMenu(panelParam);
        }

        IEnumerator LoadImage()
        {
            yield return new WaitForSeconds(2f);
            loadingImage.SetActive(false);
        } 
    }
}