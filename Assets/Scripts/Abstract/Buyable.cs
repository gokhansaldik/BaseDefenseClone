using Enums;

namespace Abstract
{
    public abstract class Buyable
    {
        public PayType PayType;
        
        public int Cost;

        public int PayedAmount;

        protected Buyable(PayType payType, int cost, int payedAmount)
        {
            PayType = payType;
            Cost = cost;
            PayedAmount = payedAmount;
        }
    }
}