using UnityEngine;
using Data.ValueObject;

[CreateAssetMenu(fileName = "CD_EnemyAI", menuName = "BaseDefence/CD_EnemyAI", order = 0)]
public class CD_EnemyAI : ScriptableObject
{
    public EnemyAiData EnemyAIData;
}