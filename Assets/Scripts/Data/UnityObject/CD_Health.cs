using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Health", menuName = "BaseDefense/CD_Health", order = 0)]
    public class CD_Health : ScriptableObject
    {
        public HealthData HealthData;
    }
}