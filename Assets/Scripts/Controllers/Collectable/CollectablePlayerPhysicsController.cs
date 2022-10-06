using System;
using Controllers.Mine;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectablePlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject collectablePlayer;
        [SerializeField] private GameObject miner;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private MinePhysicsController minePhysicsController;
        [SerializeField] private CollectableManager collectableManager;
        #endregion

        #endregion

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineArea")&& minePhysicsController._minerCount <5)
            {
                //stackManager.RemoveStackCollectable();
                collectablePlayer.SetActive(false);
                miner.SetActive(true);
               
                Debug.Log("Girdik");
                
                StackSignals.Instance.onCollectablePlayerMiner.Invoke();
            }

            if (other.CompareTag("Player") && collectableManager.IsTaken == false)
            {
               // StackSignals.Instance.onCollectablePlayerTaken.Invoke();
                collectableManager.SetAnimState(CollectableAnimationStates.Taken);
                collectableManager.IsTaken = true;
            }
        }
       
        // private void RemoveStackCollectable(GameObject gameObject)
        // {
        //     // if (stackManager._stackList.Count >0)
        //     // {
        //     //     var lastHostage = stackManager._stackList.Count - 1;
        //     //     stackManager._stackList.RemoveAt(lastHostage);
        //     //     //stackManager._stackList.TrimExcess();
        //     // }
        //     
        //     stackManager._stackList.RemoveAt(stackManager._stackList.Count-1);
        //
        //    // Destroy(gameObject);
        //     
        //     // for (int i = 0; i < stackManager._stackList.Count; i++)
        //     // {
        //     //     stackManager._stackList.RemoveAt(stackManager._stackList.Count);
        //     // }
        //
        //
        // }
      
    }
}