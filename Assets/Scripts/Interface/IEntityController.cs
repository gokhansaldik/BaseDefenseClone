using UnityEngine;

namespace Interface
{
    public interface IEntityController 
    {
     public Transform transform { get; }
      IMover Mover { get; }
    }
}
