using TMPro;
using UnityEngine;

public class ResourcesInventory : MonoBehaviour
{
    public static ResourcesInventory Instance;

    [Header("Settings")]
    [SerializeField] private int startingGems = 50;

    [Header("References")]
    [SerializeField] private TMP_Text woodQtyText;
    [SerializeField] private TMP_Text stoneQtyText;
    [SerializeField] private TMP_Text metalQtyText;
    [SerializeField] private TMP_Text gemsQtyText;

    [Header("Read Only")]
    [SerializeField] private int woodQty = 0;
    [SerializeField] private int stoneQty = 0;
    [SerializeField] private int metalQty = 0;
    [SerializeField] private int gemsQty = 0;

    public int WoodQty => woodQty;
    public int StoneQty => stoneQty;
    public int MetalQty => metalQty;
    public int GemsQty => gemsQty;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gemsQty = startingGems;

        woodQtyText.text = $"{woodQty}";
        stoneQtyText.text = $"{stoneQty}";
        metalQtyText.text = $"{metalQty}";
        gemsQtyText.text = $"{gemsQty}";
    }

    public void AddResource(ResourceType type, int qty)
    {
        switch (type)
        {
            case ResourceType.Wood:
                {
                    woodQty += qty;
                    woodQtyText.text = $"{woodQty}";
                    break;
                }
            case ResourceType.Stone:
                {
                    stoneQty += qty;
                    stoneQtyText.text = $"{stoneQty}";
                    break;
                }
            case ResourceType.Metal:
                {
                    metalQty += qty;
                    metalQtyText.text = $"{metalQty}";
                    break;
                }
            case ResourceType.Gems:
                {
                    gemsQty += qty;
                    gemsQtyText.text = $"{gemsQty}";
                    break;
                }
        }
    }

    public bool RemoveResource(ResourceType type, int qty)
    {
        switch (type)
        {
            case ResourceType.Wood:
                {
                    if (woodQty >= qty)
                    {
                        woodQty -= qty;
                        woodQtyText.text = $"{woodQty}";
                        return true;
                    }
                    return false;
                }
            case ResourceType.Stone:
                {
                    if (stoneQty >= qty)
                    {
                        stoneQty -= qty;
                        stoneQtyText.text = $"{stoneQty}";
                        return true;
                    }
                    return false;
                }
            case ResourceType.Metal:
                {
                    if (woodQty >= qty)
                    {
                        metalQty -= qty;
                        metalQtyText.text = $"{metalQty}";
                        return true;
                    }
                    return false;
                }
            case ResourceType.Gems:
                {
                    if (gemsQty >= qty)
                    {
                        gemsQty -= qty;
                        gemsQtyText.text = $"{gemsQty}";
                        return true;
                    }
                    return false;
                }
            default: return false;
        }
    }

}
