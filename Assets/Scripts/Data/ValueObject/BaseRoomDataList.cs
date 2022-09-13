using System;
using System.Collections.Generic;


namespace Data.ValueObject
{
    [Serializable]
    public class BaseRoomDataList 
    {
        public List<RoomData> BaseRooms = new List<RoomData>();
    }
}