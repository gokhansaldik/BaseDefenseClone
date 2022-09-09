using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class BuyablesData
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayedAmount;
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;
        public int BoughtMoneyWorkerAmount;
        public int BoughtAmmoWorkerAmount;
        public bool UpgradeButtonLocked;
        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;
    }
}