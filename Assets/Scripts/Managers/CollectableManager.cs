using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableAnimationController collectableAnimationController;
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] private CollectableManager collectableManager;

        #endregion

        #endregion


        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
        }
    }
}