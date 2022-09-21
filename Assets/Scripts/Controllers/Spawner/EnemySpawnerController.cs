using System;
using Controllers.Enemy;
using Data.UnityObject;
using UnityEngine;

namespace Controllers.Spawner
{
    public class EnemySpawnerController : MonoBehaviour
    {
        // #region Self Variables
        //
        // #region Serialized Variables
        //
        // [SerializeField] private GameObject redEnemyPrefab;
        // [Range(1f, 5f)] [SerializeField] private float min = 1f;
        // [Range(2f, 10f)] [SerializeField] private float max = 10f;
        //
        // #endregion
        //
        // #region Private Variables
        //
        // private float _maxSpawnTime;
        //
        // private float _currentSpawnTime = 0f;
        //
        // #endregion
        //
        // #endregion
        //
        //
        // private void OnEnable()
        // {
        //     GetRandomMaxTime();
        // }
        //
        // private void Update()
        // {
        //     _currentSpawnTime += Time.deltaTime;
        //     if (_currentSpawnTime > _maxSpawnTime)
        //     {
        //         EnemySpawn();
        //         // currentSpawnTime maxSpawnTime'dan buyukse calisacak ve surekli calısmamasi icin currentSpawnTime = 0 olacak.
        //     }
        // }
        //
        // private void EnemySpawn()
        // {
        //    GameObject newRedEnemy = Instantiate(redEnemyPrefab, transform.position, transform.rotation);  // parent ayarlamak icin yeni objeye atadık. 
        //    newRedEnemy.transform.parent = this.transform;  // atadigimiz objenin parent'ini degistirdik
        //     _currentSpawnTime = 0f;
        //     GetRandomMaxTime();
        // }
        //
        // private void GetRandomMaxTime()
        // {
        //     _maxSpawnTime = Random.Range(min, max);
        // }
        [SerializeField] private CD_SpawnInfo spawnInfo;
        private float _currentTime = 0f;
        private float _maxTime;

        private void Start()
        {
            _maxTime = spawnInfo.SpawnInfoData.RandomSpawnMaxTime;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxTime)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
           Instantiate(spawnInfo.SpawnInfoData.enemyPrefab, transform.position, Quaternion.identity);
           _currentTime = 0f;
           _maxTime = spawnInfo.SpawnInfoData.RandomSpawnMaxTime;
        }
    }
}