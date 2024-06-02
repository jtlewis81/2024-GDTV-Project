using UnityEngine;

public class DropGems : MonoBehaviour
{
    [SerializeField] private int minGemDrop = 1;
    [SerializeField] private int maxGemDrop = 5;

    public void Drop()
    {
        int drop = Random.Range(minGemDrop, maxGemDrop + 1);
        ResourcesInventory.Instance.AddResource(ResourceType.Gems, drop);
    }
}
