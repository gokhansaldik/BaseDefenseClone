using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace TryScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject redEnemyPrefab;
        private float minSpawnTime = 1f;
        private float maxSpawnTime = 2f;
        private float xMin = -27f;
        private float xMax = 27f;

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                GameObject enemy = EnemyPool.instance.GetEnemyFromPool(transform);
                enemy.transform.position = new Vector3(Random.Range(xMin, xMax), 1f, 8f);
                //Instantiate(redEnemyPrefab, new Vector3(Random.Range(xMin, xMax), 1f, 8f), quaternion.identity);
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }
    }
}
