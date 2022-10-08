using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretStackPhysicsController : MonoBehaviour
    {
        [SerializeField] private TurretManager manager;
        private List<Vector3> _bulletLocation;
        private int _indeks = 0;
        private void Awake()
        {
            _bulletLocation = new List<Vector3>() { 
                new Vector3(-0.4f, 0, 0.4f), 
                new Vector3(0.4f, 0, 0.4f), 
                //new Vector3(0.4f, 0, -0.4f), 
                //new Vector3(-0.4f, 0, -0.4f)
            };
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BulletBox"))
            {
                _indeks = manager.AmmoBoxList.Count;
            // manager.SetObjPosition(gameObject);
                int moddedIndeks = _indeks %_bulletLocation.Count;
              //manager.SetObjPosition(gameObject);
              //IdleGameSignals.Instance.onAddBulletBoxStack.Invoke(gameObject);
             other.transform.DOLocalMove(new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
            //other.transform.DOLocalMove(new Vector3(_bulletLocation[moddedIndeks].x, (int)(_indeks / 2) * 0.5f , _bulletLocation[moddedIndeks].z), 1f);
             StartCoroutine(ResetCollectableRotation(other.transform));
            }
        }
        private IEnumerator ResetCollectableRotation(Transform ammoBox)
        {
            yield return new WaitForSeconds(0.5f);
            ammoBox.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
