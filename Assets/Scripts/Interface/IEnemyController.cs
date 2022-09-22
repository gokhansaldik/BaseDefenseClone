using Class;
using UnityEngine;

namespace Interface
{
    public interface IEnemyController 
    {
      Transform transform { get; }
      IMover Mover { get; }
      CharacterAnimation Animation { get; }
    }
}
