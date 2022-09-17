using System;
using System.Collections.Generic;
using UnityEngine;

namespace TryScripts
{
    public class EnemyPool : MonoBehaviour
    {
        public static EnemyPool instance;
        [SerializeField] private GameObject enemyPrefab;
        private Queue<GameObject> enemyPool = new Queue<GameObject>();
        [SerializeField] private int enemyPoolSize = 10;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            for (int i = 0; i < enemyPoolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemyPool.Enqueue(enemy);
                enemy.SetActive(false);
            }
           
        }

        public GameObject GetEnemyFromPool(Transform otherTransform)
        {
            if (enemyPool.Count > 0)
            {
                otherTransform.SetParent(transform);
                GameObject enemy = enemyPool.Dequeue();
                enemy.SetActive(true);
                return enemy;
            }
            else
            {
                GameObject enemy = Instantiate(enemyPrefab);
                return enemy;
            }
        }

        public void ReturnEnemyPool(GameObject enemy)
        {
            //enemy.transform.parent = transform.GetChild((int))
            enemyPool.Enqueue(enemy);
            enemy.SetActive(false);
        }
    }
}
