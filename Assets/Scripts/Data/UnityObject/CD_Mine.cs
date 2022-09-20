using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MineData", menuName = "BaseDefense/CD_MineData", order = 0)]
    public class CD_Mine : ScriptableObject
    {
        public MineStackData MineStackData;
    }
}