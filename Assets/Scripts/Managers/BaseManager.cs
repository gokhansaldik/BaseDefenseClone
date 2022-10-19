using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public BaseData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private List<RoomManager> Rooms = new List<RoomManager>();

        #endregion

        #region Private Variables

        [ShowInInspector] private AreaDataParams _areaDatas;
        private Dictionary<TurretNameEnum, int> _payedTurretDatas;
        private Dictionary<RoomNameType, int> _payedRoomDatas;
        private int _baseLevel;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleGameSignals.Instance.onBaseAreaBuyedItem += OnSetAreaDatas;
            SaveSignals.Instance.onSaveAreaData += OnGetAreaDatas;
            IdleGameSignals.Instance.onTurretData += OnGetTurretData;
        }

        private void UnsubscribeEvents()
        {
            IdleGameSignals.Instance.onBaseAreaBuyedItem -= OnSetAreaDatas;
            SaveSignals.Instance.onSaveAreaData -= OnGetAreaDatas;
            IdleGameSignals.Instance.onTurretData -= OnGetTurretData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _baseLevel = LevelSignals.Instance.onGetLevelID();
            _areaDatas = SaveSignals.Instance.onLoadAreaData();
            Data = Resources.Load<CD_Level>("Data/CD_Level").LevelData[_baseLevel].BaseData;
            SetBaseLevelText();
            EmptyListCheck();
            SetRoomData();
        }

        private void EmptyListCheck()
        {
            if (!_areaDatas.RoomPayedAmount.IsNullOrEmpty()) return;
            _areaDatas.RoomPayedAmount = new List<int>(new int[Data.BaseRoomData.BaseRooms.Count]);
            _areaDatas.RoomTurretPayedAmount = new List<int>(new int[Data.BaseRoomData.BaseRooms.Count]);
        }

        private void SetRoomData()
        {
            for (var i = 0; i < Rooms.Count; i++)
                Rooms[i].SetRoomData(Data.BaseRoomData.BaseRooms[i], _areaDatas.RoomPayedAmount[i]);
        }

        private void OnSetAreaDatas()
        {
            for (var i = 0; i < Rooms.Count; i++) _areaDatas.RoomPayedAmount[i] = Rooms[i].PayedAmount;

            SaveSignals.Instance.onAreaDataSave?.Invoke();
            SaveSignals.Instance.onScoreSave?.Invoke();
        }

        private AreaDataParams OnGetAreaDatas()
        {
            return _areaDatas;
        }

        private void SetBaseLevelText()
        {
            textMeshPro.text = "Base " + (SaveSignals.Instance.onLoadCurrentLevel() + 1);
        }

        private TurretData OnGetTurretData(TurretNameEnum turret)
        {
            return Data.BaseRoomData.BaseRooms[(int)turret].TurretData;
        }
    }
}