using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;
namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_GunPrices", menuName = "BaseDefense/CD_GunPrices", order = 0)]
    public class CD_GunPrices : ScriptableObject
    {
        public ItemPricesData Data;

    }
}