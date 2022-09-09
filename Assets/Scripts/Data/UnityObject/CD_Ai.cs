using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AI", menuName = "BaseDefense/CD_AI", order = 0)]
    public class CD_Ai : ScriptableObject
    {
        public AmmoWorkerData AmmoWorkerAIData;
        public MoneyWorkerData MoneyWorkerAIData;
        public MineWorkerData MineWorkerAIData;
        public SoldierData SoldierAIData;
        public EnemyAiData EnemyAIData;
    }
}