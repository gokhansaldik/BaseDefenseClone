using System;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseData
    {
        public BaseRoomDataList BaseRoomData;
        public MineBaseData MineBaseData;
        public MilitaryBaseData MilitaryBaseData;
        public BuyablesData BuyablesData;
    }
}