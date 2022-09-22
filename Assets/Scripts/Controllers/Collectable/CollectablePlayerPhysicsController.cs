using System;
using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectablePlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] private GameObject collectablePlayer;
        [SerializeField] private GameObject miner;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineArea"))
            {
                collectablePlayer.SetActive(false);
                miner.SetActive(true);
            }
        }
    }
}