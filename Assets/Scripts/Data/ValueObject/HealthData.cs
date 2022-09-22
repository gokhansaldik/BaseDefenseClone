using System;

namespace Data.ValueObject
{
    [Serializable]
    public class HealthData
    {
        public int maxHealth;
        public int MaxHealth => maxHealth;
    }
}