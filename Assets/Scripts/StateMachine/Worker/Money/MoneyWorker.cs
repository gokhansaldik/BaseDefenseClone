using Inheritance;

namespace StateMachine.Worker.Money
{
    public class MoneyWorker : AiBase
    {
        public override void Collect()
        {
            base.Collect();
            if (MoneyList != null)
            {
                aiNavmesh.SetDestination(MoneyList[0].transform.position);
            }
            if (CollectedMoneyList.Count >= MoneyWorkerCollectLimit)
            {
                GoToTarget(BaseInTransform);
            }
        }
    }
}