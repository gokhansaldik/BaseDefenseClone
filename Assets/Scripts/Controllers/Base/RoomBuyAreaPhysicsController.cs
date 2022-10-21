using System;
using Managers;
using UnityEngine;

namespace Controllers.Base
{
    public class RoomBuyAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;

        #endregion
        [SerializeField] private ParticleSystem _buyParticle;
        #endregion

       

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) roomManager.BuyAreaEnter();
            _buyParticle.Play();
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) roomManager.BuyAreaExit();
            _buyParticle.Stop();
        }
    }
}