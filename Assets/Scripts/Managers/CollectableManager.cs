using Controllers;
using Enums;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableAnimationController collectableAnimationController;

        #endregion

        #endregion


        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
        }
    }
}