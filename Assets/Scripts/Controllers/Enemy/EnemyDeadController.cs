using System.Collections;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyDeadController : MonoBehaviour
    {
        private readonly float _delayTime = 5f;

        public void DeadAction()
        {
            StartCoroutine(DeadActionAsync());
        }

        private IEnumerator DeadActionAsync()
        {
            yield return new WaitForSeconds(_delayTime);

            // Destroy(this.gameObject);
        }
    }
}