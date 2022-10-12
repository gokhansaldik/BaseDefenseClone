using System;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private ScoreDataParams _loadedScoreData;
        private readonly ScoreData _scoreData = new ScoreData();

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveScoreData += OnGetSaveScoreData;
            ScoreSignals.Instance.onScoreData += OnGetScoreData;
            ScoreSignals.Instance.onSetScore += OnSetScore;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveScoreData -= OnGetSaveScoreData;
            ScoreSignals.Instance.onScoreData -= OnGetScoreData;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _loadedScoreData = SaveSignals.Instance.onLoadScoreData();
            _scoreData.MoneyScore = _loadedScoreData.MoneyScore;
            _scoreData.DiamondScore = _loadedScoreData.GemScore;
            ScoreSignals.Instance.onSetScoreToUI?.Invoke();
        }

        private void OnSetScore(PayType scoreType, int score)
        {
            switch (scoreType)
            {
                case PayType.Money:
                    _scoreData.MoneyScore += score;
                    break;
                case PayType.Gem:
                    _scoreData.DiamondScore += score;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scoreType), scoreType, null);
            }

            ScoreSignals.Instance.onSetScoreToUI?.Invoke();
        }

        private ScoreDataParams OnGetSaveScoreData()
        {
            return new ScoreDataParams
            {
                MoneyScore = _scoreData.MoneyScore,
                GemScore = _scoreData.DiamondScore
            };
        }

        private ScoreDataParams OnGetScoreData()
        {
            return new ScoreDataParams
            {
                MoneyScore = _scoreData.MoneyScore,
                GemScore = _scoreData.DiamondScore
            };
        }
    }
}