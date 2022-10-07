using Inheritance;

namespace StateMachine.Worker.Ammo
{
    public class AmmoWorker : AiBase
    {
        public override void Collect()
        {
            base.Collect();
            if (AmmoList != null)
            {
                aiNavmesh.SetDestination(AmmoList[0].transform.position);
            }
            if (CollectedAmmoList.Count >= AmmoWorkerCollectLimit)
            {
                GoToTarget(BaseTurretTransform);
            }
        }
    }
}