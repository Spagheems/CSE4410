using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image ammoIcon;
    [SerializeField] private Text ammoCount;

    [Header("Settings")]
    [SerializeField] private Sprite healthFull;
    [SerializeField] private Sprite healthEmpty;
    [SerializeField] private Sprite ammoSprite;

    private int maxHealth = 100;
    private int currentHealth;
    private int maxAmmo = 30;
    private int currentAmmo;

    void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateUI();
    }

    public void Reload(int ammo)
    {
        currentAmmo = Mathf.Min(currentAmmo + ammo, maxAmmo);
        UpdateUI();
    }

    public void Fire()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.sprite = currentHealth > maxHealth / 2 ? healthFull : healthEmpty;
        }

        if (ammoIcon != null)
        {
            ammoIcon.sprite = ammoSprite;
        }

        if (ammoCount != null)
        {
            ammoCount.text = currentAmmo.ToString();
        }
    }
} 
