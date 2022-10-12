using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AmmoWorker", menuName = "BaseDefense/CD_AmmoWorker", order = 0)]
    public class CD_AmmoWorker : ScriptableObject
    {
        public WorkerData WorkerData;
    }
}