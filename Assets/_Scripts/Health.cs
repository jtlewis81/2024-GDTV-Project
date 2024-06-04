using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public int CurrentHP { get; set; }


    private void Awake()
    {
        CurrentHP = MaxHP;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if(CurrentHP <= 0)
        {
            if(GetComponent<Gate>() != null)
            {
                GameManager.GameOver = true;
            }

            DropGems drop = GetComponent<DropGems>();
            if(drop != null)
            {
                GameManager.EnemyCount--;
                drop.Drop();
            }

            Destroy(gameObject);
        }
        UpdateUI();
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
        UpdateUI();
    }
    
    public void Upgrade(int amount)
    {
        MaxHP += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(hpBar != null)
        {
            hpBar.value = (float)CurrentHP / MaxHP;
        }
        if(hpText != null)
        {
            hpText.text = $"{CurrentHP} / {MaxHP}";
        }
    }
}
