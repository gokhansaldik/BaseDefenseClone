using UnityEngine;

namespace Controllers.Spawner
{
    public class EnemySpawnerController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject redEnemyPrefab;
        [Range(1f, 5f)] [SerializeField] private float min = 1f;
        [Range(2f, 10f)] [SerializeField] private float max = 10f;

        #endregion

        #region Private Variables

        private float _maxSpawnTime;

        private float _currentSpawnTime = 0f;

        #endregion

        #endregion


        private void OnEnable()
        {
            GetRandomMaxTime();
        }

        private void Update()
        {
            _currentSpawnTime += Time.deltaTime;
            if (_currentSpawnTime > _maxSpawnTime)
            {
                EnemySpawn();
                // currentSpawnTime maxSpawnTime'dan buyukse calisacak ve surekli calısmamasi icin currentSpawnTime = 0 olacak.
            }
        }

        private void EnemySpawn()
        {
           GameObject newRedEnemy = Instantiate(redEnemyPrefab, transform.position, transform.rotation);  // parent ayarlamak icin yeni objeye atadık. 
           newRedEnemy.transform.parent = this.transform;  // atadigimiz objenin parent'ini degistirdik
            _currentSpawnTime = 0f;
            GetRandomMaxTime();
        }

        private void GetRandomMaxTime()
        {
            _maxSpawnTime = Random.Range(min, max);
        }
    }
}