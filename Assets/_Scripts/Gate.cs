using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Gate : IInteractable
{
    [Header("References")]
    [SerializeField] private Transform menuPanel;
    [SerializeField] private TMP_Text upgradeCostText;

    [Header("Settings")]
    [SerializeField] private int initUpgradeCost = 250;
    [SerializeField] private int upgradeCostMultiplier = 5;
    [SerializeField] private int hpUpgradeMultiplier = 2;

    [Header("Read Only")]
    [SerializeField] private int currentUpgradeCost;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        currentUpgradeCost = initUpgradeCost;
        upgradeCostText.text = $"{initUpgradeCost}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>() != null)
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

    public void Upgrade()
    {
        if (ResourcesInventory.Instance.RemoveResource(ResourceType.Gems, currentUpgradeCost))
        {
            currentUpgradeCost *= upgradeCostMultiplier;
            upgradeCostText.text = $"{currentUpgradeCost}";

            health.MaxHP *= hpUpgradeMultiplier;
            health.UpdateUI();
        }
    }

    public void Repair(int amount)
    {
        if (ResourcesInventory.Instance.RemoveResource(ResourceType.Gems, amount))
        {
            health.CurrentHP += amount;
            health.UpdateUI();
        }
    }
}
