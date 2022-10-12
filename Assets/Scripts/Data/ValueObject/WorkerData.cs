using System;

namespace Data.ValueObject
{
    [Serializable]
    public class WorkerData
    {
        public WorkerStackData WorkerStackData;
    }
}
[Serializable]
public class WorkerStackData
{
    public int StackLimit = 10;
    public float StackoffsetY = 10;
    public float StackoffsetZ = 10;
    public float AnimationDurition = 1;
}