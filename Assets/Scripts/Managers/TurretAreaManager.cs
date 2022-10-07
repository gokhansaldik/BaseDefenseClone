using System;
using System.Collections;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TurretAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretNameEnum turretName;
        [SerializeField] private TextMeshPro textMeshPro;

        #endregion

        #region Private Variables

        private TurretData _turretData;
        private int _payedAmount;
        private int _remainingAmount;
        private ScoreDataParams _scoreCache;
        private GameObject _textParentGameObject;

        private int PayedAmount
        {
            get => _payedAmount;
            set
            {
                _payedAmount = value;
                _remainingAmount = _turretData.Cost - _payedAmount;
                if (_remainingAmount == 0)
                {
                    _textParentGameObject.SetActive(false);
                }
                else
                {
                    SetText(_remainingAmount);
                }
            }
        }
        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
        }
        private void GetReferences()
        {
            _textParentGameObject = textMeshPro.transform.parent.gameObject;
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            OnSetData();
        }

        private void SubscribeEvents()
        {
            IdleGameSignals.Instance.onGettedBaseData += OnSetData;
        }

        private void UnsubscribeEvents()
        {
            IdleGameSignals.Instance.onGettedBaseData -= OnSetData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnSetData()
        {
            _turretData = IdleGameSignals.Instance.onTurretData(turretName);
            PayedAmount = IdleGameSignals.Instance.onPayedTurretData(turretName);
        }
        public void BuyAreaEnter()
        {
            _scoreCache = ScoreSignals.Instance.onScoreData();
            switch (_turretData.PayType)
            {
                case PayType.Money:
                    if (_scoreCache.MoneyScore > _remainingAmount)
                    {
                        StartCoroutine(Buy());
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void BuyAreaExit()
        {
            StopAllCoroutines();
            IdleGameSignals.Instance.onTurretAreaBuyedItem?.Invoke(turretName, _payedAmount);
            SaveSignals.Instance.onAreaDataSave?.Invoke();
        }
        private IEnumerator Buy()
        {
            var waitForSecond = new WaitForSeconds(0.05f);
            while (_remainingAmount > 0)
            {
                PayedAmount++;
                ScoreSignals.Instance.onSetScore?.Invoke(_turretData.PayType, -1);
                SaveSignals.Instance.onScoreSave?.Invoke();
                yield return waitForSecond;
            }
            IdleGameSignals.Instance.onTurretAreaBuyedItem?.Invoke(turretName, _payedAmount);
        }
        private void SetText(int remainingAmount)
        {
            textMeshPro.text = remainingAmount.ToString();
        }
    }
}