using Commands.Level;
using Data.UnityObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private ClearActiveLevelCommand _levelClearer;
        [ShowInInspector] private int _levelID;

        #endregion

        #endregion

        private void Awake()
        {
            _levelClearer = new ClearActiveLevelCommand(ref levelHolder);
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onGetLevelID += OnGetLevelID;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onGetLevelID -= OnGetLevelID;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _levelID = SaveSignals.Instance.onLoadCurrentLevel();
            OnInitializeLevel();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private void OnInitializeLevel()
        {
            var newLevelData = GetLevelCount();
            _levelLoader.Execute(newLevelData);
        }

        private int GetLevelCount()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.Count;
        }
    }
}