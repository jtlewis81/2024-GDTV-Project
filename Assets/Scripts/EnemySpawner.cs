using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Enemy> enemyPrefabs= new List<Enemy>();

    [Header("Settings")]
    [SerializeField] private float spawnFrequency;
    [SerializeField] private int spawnQty;

    private float spawnTimer = 0;
    
    [field:SerializeField] public bool IsActive { get; set; } = false;

    private void Awake()
    {
        spawnTimer = spawnFrequency;
    }

    private void Update()
    {
        if (IsActive)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = spawnFrequency;
            }
        }
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnQty; i++)
        {
            Enemy enemy = Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity, null);
        }
    }

}
