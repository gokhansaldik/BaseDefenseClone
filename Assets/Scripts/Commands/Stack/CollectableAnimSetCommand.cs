using Enums;
using Managers;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableAnimSetCommand
    {
        public void Execute(GameObject collectable, CollectableAnimationStates states)
        {
            CollectableManager _collectableManager = collectable.transform.GetComponent<CollectableManager>();
            _collectableManager.SetAnimState(states);
        }
    }
}