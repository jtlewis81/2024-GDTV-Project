using TMPro;
using UnityEngine;

public class ResourceBuilding : IInteractable
{
    [Header("References")]
    [SerializeField] private Transform menuPanel;
    [SerializeField] private TMP_Text unlockCostText;
    [SerializeField] private TMP_Text productionRateText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Transform unlockPanel;
    [SerializeField] private Transform upgradePanel;

    [Header("Settings")]
    [SerializeField] private ResourceType resourceType = ResourceType.Wood;
    [SerializeField] private int unlockCost = 50;
    [SerializeField] private int initProductionRate = 1;
    [SerializeField] private int initUpgradeCost = 100;
    [SerializeField] private int productionUpgradeMultiplier = 3;
    [SerializeField] private int upgradeCostMultiplier = 5;

    [Header("Read Only")]
    [SerializeField] private int currentUpgradeCost;
    [SerializeField] private int currentProductionRate;
    [SerializeField] private bool isUnlocked = false;


    private float timer = 1f;
    private float currTime = 0f;

    // maybe add max production rate/level and disable upgrade panel when reached?
    // this could be set differently per game level for difficulty?

    private void Start()
    {
        currentUpgradeCost = initUpgradeCost;
        currentProductionRate = initProductionRate;

        unlockCostText.text = $"{unlockCost} Gems";
        upgradeCostText.text = $"{initUpgradeCost}";
    }

    private void Update()
    {
        if (isUnlocked)
        {
            currTime += Time.deltaTime;

            if (currTime >= timer)
            {
                currTime = 0f;

                ResourcesInventory.Instance.AddResource(resourceType, currentProductionRate);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerActions>() != null)
        {
            menuPanel.gameObject.SetActive(true);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>() != null)
        {
            menuPanel.gameObject.SetActive(false);
        }
    }

    public void Unlock()
    {
        if (ResourcesInventory.Instance.RemoveResource(ResourceType.Gems, unlockCost))
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
        if (ResourcesInventory.Instance.RemoveResource(ResourceType.Gems, currentUpgradeCost))
        {
            currentUpgradeCost *= upgradeCostMultiplier;
            upgradeCostText.text = $"{currentUpgradeCost}";

            currentProductionRate *= productionUpgradeMultiplier;
            productionRateText.text = $"{currentProductionRate}";
        }

    }
}
