using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Settings")]
    [SerializeField] private int initUpgradeCost = 50;
    [SerializeField] private int upgradeCostMultiplier = 4;
    [SerializeField] private float initAttackRange = 15;
    [SerializeField] private float initAttackSpeed = 1;
    [SerializeField] private int initAttackPower = 10;
    [SerializeField] private float attackRangeUpgradeMultiplier = 1.2f;
    [SerializeField] private float attackSpeedUpgradeMultiplier = 0.8f;
    [SerializeField] private int attackPowerUpgradeMultiplier = 2;
    [SerializeField] private int levelCap = 4;

    [Header("Read Only")]
    [SerializeField] private int currentUpgradeCost;
    [SerializeField] private float currentAttackRange;
    [SerializeField] private float currentAttackSpeed;
    [SerializeField] private int currentAttackPower;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private List<Enemy> enemyList;

    private SphereCollider targetAreaCollider;

    public int CurrentUpgradeCost { get { return currentUpgradeCost; } set { currentUpgradeCost = value; } }
    public bool IsCapped = false;

    private float attackTimer;

    private void OnEnable()
    {
        enemyList = new List<Enemy>();
        attackTimer = initAttackSpeed;
        targetAreaCollider = GetComponent<SphereCollider>();
        targetAreaCollider.radius = initAttackRange;
        currentUpgradeCost = initUpgradeCost;
        currentAttackRange = initAttackRange;
        currentAttackSpeed = initAttackSpeed;
        currentAttackPower = initAttackPower;
    }

    private void Update()
    {
        if (ValidateEnemyList())
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = currentAttackSpeed;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemyList.Add(enemy);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (other != null)
        {
            enemyList.Remove(enemy);
        }
    }

    public void Upgrade()
    {
        if (currentLevel < levelCap)
        {
            currentAttackPower *= attackPowerUpgradeMultiplier;
            currentAttackSpeed *= attackSpeedUpgradeMultiplier;
            currentAttackRange *= attackRangeUpgradeMultiplier;
            currentLevel++;
            if (currentLevel == levelCap)
            {
                IsCapped = true;
            }
            else
            {
                currentUpgradeCost *= upgradeCostMultiplier;
            }
            
        }
    }

    private void Attack()
    {
        ValidateEnemyList();
        if (enemyList.Count > 0 && enemyList[0] != null)
        {
            Transform target = enemyList[0].transform;
            Vector3 dir = ((target.position + Vector3.up) - firePoint.position).normalized;
            Projectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(dir, Vector3.up), null);
            projectile.Damage = currentAttackPower;
        }
    }

    private bool ValidateEnemyList()
    {
        for(int i = 0; i < enemyList.Count - 1; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }
        if (enemyList.Count > 0) return true;

        return false;
    }
}
