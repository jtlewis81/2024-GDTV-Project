using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    [Header("Settings")]
    [SerializeField] private float maxHP;

    [Header("Read Only")]
    [SerializeField] private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
        UpdateUI();
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateUI();
    }
    
    public void Upgrade(float amount)
    {
        maxHP += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(hpBar != null)
        {
            hpBar.value = currentHP / maxHP;
        }
        if(hpText != null)
        {
            hpText.text = $"{currentHP} / {maxHP}";
        }
    }
}
