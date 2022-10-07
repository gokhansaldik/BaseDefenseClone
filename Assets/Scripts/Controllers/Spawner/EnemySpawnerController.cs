using Controllers.Enemy;
using Data.UnityObject;
using Managers;
using UnityEngine;

namespace Controllers.Spawner
{
    public class EnemySpawnerController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CD_SpawnInfo spawnInfo;
        [SerializeField] private GameObject enemyTarget;

        #endregion

        #region Private Variables

        private float _maxTime;
        private float _currentTime = 0f;

        #endregion
        #endregion

        private void Start()
        {
            _maxTime = spawnInfo.SpawnInfoData.RandomSpawnMaxTime;
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxTime && EnemyManager.Instance.CanSpawn)
            {
                SpawnEnemy();
            }
        }
        private void SpawnEnemy()
        {
            EnemyController enemyController = Instantiate(spawnInfo.SpawnInfoData.enemyPrefab, transform.position,
                Quaternion.identity);
            EnemyManager.Instance.AddEnemyController(enemyController);
            enemyController.EnemyTarget = enemyTarget;
            _currentTime = 0f;
            _maxTime = spawnInfo.SpawnInfoData.RandomSpawnMaxTime;
        }
    }
}