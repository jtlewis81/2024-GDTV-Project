using System.Collections.Generic;
using UnityEngine;

public class TowerBase : IInteractable
{
    [SerializeField] private Transform menuPanel;
    [SerializeField] private Transform buildPanel;
    [SerializeField] private Transform managePanel;
    [SerializeField] private Transform towerHolder;
    [SerializeField] private List<Tower> towerPrefabs;

    private Tower currentTower;

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

    public void BuildTower(int prefabIndex)
    {
        switch (prefabIndex)
        {
            case 0:
                {
                    if (TrySpendResources(25, 0, 0))
                    {
                        currentTower = Instantiate(towerPrefabs[prefabIndex], towerHolder);
                        buildPanel.gameObject.SetActive(false);
                        managePanel.gameObject.SetActive(true);
                    }                    
                    break;
                }
            case 1:
                {
                    if (TrySpendResources(200, 50, 0))
                    {
                        currentTower = Instantiate(towerPrefabs[prefabIndex], towerHolder);
                        buildPanel.gameObject.SetActive(false);
                        managePanel.gameObject.SetActive(true);
                    }
                    break;
                }
                /*
            case 2:
                {
                    if (TrySpendResources(500, 1000, 100))
                    {
                        currentTower = Instantiate(towerPrefabs[prefabIndex], towerHolder);
                        buildPanel.gameObject.SetActive(false);
                        managePanel.gameObject.SetActive(true);
                    }
                    break;
                }
            case 3:
                {
                    if (TrySpendResources(1000, 0, 500))
                    {
                        currentTower = Instantiate(towerPrefabs[prefabIndex], towerHolder);
                        buildPanel.gameObject.SetActive(false);
                        managePanel.gameObject.SetActive(true);
                    }
                    break;
                }
                */
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

    public void DestroyTower()
    {
        managePanel.gameObject.SetActive(false);
        buildPanel.gameObject.SetActive(true);
        currentTower.transform.SetParent(null);
        Destroy(currentTower.gameObject);
    }
}
