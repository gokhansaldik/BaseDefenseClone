using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Mine
{
    public class MinePhysicsController : MonoBehaviour
    {
        public int _minerCount;
        [SerializeField] private MineManager mineManager;
        [SerializeField] private StackManager stackManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected") && _minerCount <5)
            {
                _minerCount++;
                 mineManager.minerCountText.text = _minerCount.ToString() + "/"+"5";
               
            }
            
        }
       
        
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         stackManager._stackList.Clear();
        //     }
        // }

        // private void RemoveStackCollectable()
        // {
        //     if (stackManager._stackList.Count >0)
        //     {
        //         var lastHostage = stackManager._stackList.Count - 1;
        //         stackManager._stackList.RemoveAt(lastHostage);
        //         stackManager._stackList.TrimExcess();
        //     }
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