using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBase : IInteractable
{
    [Header("References")]
    [SerializeField] private Transform menuPanel;
    [SerializeField] private Transform buildPanel;
    [SerializeField] private Transform upgradePanel;
    [SerializeField] private Transform towerHolder;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private List<Tower> towerPrefabs;

    [Header("Read Only")]
    [SerializeField] private Tower currentTower;

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

    public void TryBuildTower(int prefabIndex)
    {
        switch (prefabIndex)
        {
            case 0:
                {
                    if (TrySpendResources(25, 0, 0))
                    {
                        BuildTower(prefabIndex);
                    }                    
                    break;
                }
            case 1:
                {
                    if (TrySpendResources(200, 50, 0))
                    {
                        BuildTower(prefabIndex);
                    }
                    break;
                }
            case 2:
                {
                    if (TrySpendResources(1000, 250, 100))
                    {
                        BuildTower(prefabIndex);
                    }
                    break;
                }
            case 3:
                {
                    if (TrySpendResources(0, 1000, 500))
                    {
                        BuildTower(prefabIndex);
                    }
                    break;
                }
            default: break;
        }

    }

    private bool TrySpendResources(int wood, int stone, int metal)
    {
        bool hasStone = stone <= ResourcesInventory.Instance.StoneQty;
        bool hasWood = wood <= ResourcesInventory.Instance.WoodQty;
        bool hasMetal = metal <= ResourcesInventory.Instance.MetalQty;

        if (hasStone && hasWood && hasMetal)
        {
            ResourcesInventory.Instance.RemoveResource(ResourceType.Stone, stone);
            ResourcesInventory.Instance.RemoveResource(ResourceType.Wood, wood);
            ResourcesInventory.Instance.RemoveResource(ResourceType.Metal, metal);
            return true;
        }

        return false;
    }

    private void BuildTower(int prefabIndex)
    {
        currentTower = Instantiate(towerPrefabs[prefabIndex], towerHolder);
        upgradeCostText.text = $"{currentTower.CurrentUpgradeCost}";
        buildPanel.gameObject.SetActive(false);
        upgradePanel.gameObject.SetActive(true);
    }

    public void Upgrade()
    {
        if(!currentTower.IsCapped && ResourcesInventory.Instance.RemoveResource(ResourceType.Gems, currentTower.CurrentUpgradeCost))
        {
            currentTower.Upgrade();
            if(!currentTower.IsCapped)
            {
                upgradeCostText.text = $"{currentTower.CurrentUpgradeCost}";
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
            }
        }
    }

    public void DestroyTower()
    {
        upgradeButton.gameObject.SetActive(true);
        upgradePanel.gameObject.SetActive(false);
        buildPanel.gameObject.SetActive(true);
        currentTower.transform.SetParent(null);
        Destroy(currentTower.gameObject);
    }
}
