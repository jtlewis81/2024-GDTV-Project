using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Enemy> enemyPrefabs= new List<Enemy>();

    [Header("Settings")]
    [SerializeField] private float initDelay = 10f;
    [SerializeField] private float spawnFrequency = 2f;
    [SerializeField] private int spawnQty = 1;

    private int stage = 0;
    private float spawnTimer = 0;

    private void Awake()
    {
        spawnTimer = initDelay;
    }

    private void Update()
    {   
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            if (!GameManager.GameOver && GameManager.EnemyCount < GameManager.EnemyCap)
            {
                Spawn();
            }
            spawnTimer = spawnFrequency;
        }        
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnQty; i++)
        {
            Enemy enemy = Instantiate(enemyPrefabs[stage], transform.position, Quaternion.identity, null);
            GameManager.EnemyCount++;
        }
    }

}
