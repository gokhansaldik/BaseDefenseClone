using System;
using System.Collections;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int PayedAmount
        {
            get => _payedAmount;
            set
            {
                _payedAmount = value;
                _remainingAmount = _roomData.Cost - _payedAmount;
                if (_remainingAmount == 0)
                {
                    _textParentGameObject.SetActive(false);
                    area.SetActive(true);
                    fence.SetActive(false);
                }
                else
                {
                    SetText(_remainingAmount);
                }
            }
        }

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject area;
        [SerializeField] private GameObject fence;
        [SerializeField] private TextMeshPro tmp;

        #endregion

        #region Private Variables

        [ShowInInspector] private RoomData _roomData;
        private bool _isBase;
        private int _payedAmount;
        private int _remainingAmount;
        private ScoreDataParams _scoreCache;
        private GameObject _textParentGameObject;
        
        #endregion

        #endregion

        private void Awake()
        {
            _textParentGameObject = tmp.transform.parent.gameObject;
           
        }

        public void SetRoomData(RoomData roomData, int payedAmount)
        {
            _roomData = roomData;
            _isBase = roomData.Base;
            if (!_isBase) PayedAmount = payedAmount;
        }
        private IEnumerator Buy()
        {
            var waitForSecond = new WaitForSeconds(0.05f);
            while (_remainingAmount > 0)
            {
                PayedAmount++;
                ScoreSignals.Instance.onSetScore?.Invoke(_roomData.PayType, -1);
                // _buyParticle.Play();
                yield return waitForSecond;
            }

            IdleGameSignals.Instance.onBaseAreaBuyedItem?.Invoke();
        }
        public void BuyAreaEnter()
        {
            _scoreCache = ScoreSignals.Instance.onScoreData();
            switch (_roomData.PayType)
            {
                case PayType.Money:
                    if (_scoreCache.MoneyScore > _remainingAmount) StartCoroutine(Buy());

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void BuyAreaExit()
        {
            StopAllCoroutines();
            IdleGameSignals.Instance.onBaseAreaBuyedItem?.Invoke();
        }

       

        private void SetText(int remainingAmound)
        {
            tmp.text = remainingAmound.ToString();
        }
    }
}