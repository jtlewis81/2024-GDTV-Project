using TMPro;
using UnityEngine;

public class LumberMill : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text productionRateText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Transform unlockPanel;
    [SerializeField] private Transform upgradePanel;
    [SerializeField] private ResourcesInventory inventory;

    [Header("Settings")]
    [SerializeField] private ResourceType resourceType = ResourceType.Wood;
    [SerializeField] private int unlockCost = 100;
    [SerializeField] private int initProductionRate = 1;
    [SerializeField] private int productionUpgradeMultiplier = 3;
    [SerializeField] private int upgradeCostMultiplier = 5;

    [Header("Read Only")]
    [SerializeField] private int currentUpgradeCost = 50;
    [SerializeField] private int currentProductionRate = 0;
    [SerializeField] private bool isUnlocked = false;

    // maybe add max production rate/level and disable upgrade panel when reached?
    // this could be set differently per game level for difficulty?

    private float timer = 1f;
    private float currTime = 0f;

    private void Update()
    {
        if (isUnlocked)
        {
            currTime += Time.deltaTime;

            if (currTime >= timer)
            {
                currTime = 0f;

                inventory.AddResource(resourceType, currentProductionRate);
            }

        }
    }

    public void Unlock()
    {
        if (inventory.RemoveResource(ResourceType.Gems, unlockCost))
        {
            isUnlocked = true;
            unlockPanel.gameObject.SetActive(false);
            upgradePanel.gameObject.SetActive(true);
            currentProductionRate = initProductionRate;
            productionRateText.text = $"{currentProductionRate}";
        }
    }

    public void Upgrade()
    {
        if(inventory.RemoveResource(ResourceType.Gems, currentUpgradeCost))
        {
            currentUpgradeCost *= upgradeCostMultiplier;
            upgradeCostText.text = $"{currentUpgradeCost}";

            currentProductionRate *= productionUpgradeMultiplier;
            productionRateText.text = $"{currentProductionRate}";
        }
        
    }
}
