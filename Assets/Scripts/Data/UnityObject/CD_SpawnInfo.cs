using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_SpawnInfo", menuName = "BaseDefense/CD_SpawnInfo", order = 0)]
    public class CD_SpawnInfo : ScriptableObject
    {
        public SpawnInfoData SpawnInfoData;
    }
}