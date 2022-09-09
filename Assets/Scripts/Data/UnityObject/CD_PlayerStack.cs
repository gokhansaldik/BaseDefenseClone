using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerStack", menuName = "BaseDefense/CD_PlayerStack", order = 0)]
    public class CD_PlayerStack : ScriptableObject
    {
        public PlayerMoneyStackData PlayerMoneyStackData;
        public PlayerAmmoStackData PlayerAmmoStackData;
        public PlayerHostageStackData PlayerHostageStackData;
    }
}