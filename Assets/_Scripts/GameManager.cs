using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<EnemySpawner> spawners;
    [field: SerializeField] public static bool GameOver = false;
    [field: SerializeField] public static int EnemyCap = 100;
    [field: SerializeField] public static int EnemyCount = 0;
    [field: SerializeField] public static int KillCount = 0;

    private int[] stages = new int[] { 0, 200, 350, 500, 600, 750 };
    private int stage = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (stage < 4)
        {  
            for (int i = 0; i < stages.Length; i++)
            {
                if(KillCount > stages[i])
                {
                    stage++;
                }
            }
        }
        
    }


}
