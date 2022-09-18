using Datas.ValueObject;
using System.Collections;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level/AIData", menuName = "Data/AIData", order = 0)]
    public class CD_AIData: ScriptableObject
    {
        // public AmmoWorker ammoWorker;
        // public MoneyWorker moneyWorker;
        // public MineWorker mineWorkeri;
        // public Soldier soldier;
        public Enemy enemy;
       
    }
}