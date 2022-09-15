using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject redEnemyPrefab;
    [SerializeField] private float redEnemyInterval = 3.5f;
   
   

    private void Start()
    {
        StartCoroutine(SpawnEnemy(redEnemyInterval, redEnemyPrefab));
    }

    IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6, 6f), 0),
            Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));

    }
}

// [Serializable]
// public class Enemy
// {
//     public GameObject EnemyPrefab;
//     public int EnemyCost;
// }