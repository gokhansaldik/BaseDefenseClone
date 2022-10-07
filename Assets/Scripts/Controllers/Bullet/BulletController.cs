using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private int bulletThrowForce = 1500;

        #endregion

        #region Private Variables
        
        private Rigidbody _rigidbody;

        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            Move();
            Destroy(gameObject, 3f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                return;
            }
        }
        private void Move()
        {
            _rigidbody.AddRelativeForce(Vector3.forward * bulletThrowForce, ForceMode.Force);
        }
    }
}