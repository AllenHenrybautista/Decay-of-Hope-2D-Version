using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarForegroundImage;
    [SerializeField] private Image _staminaBarForeGroundImage;
    [SerializeField] private RectTransform _healthBarTransform;
    
    private PlayerCombat playerCombat;
    private PlayerStats playerStats;

    private void Start()
    {
       SetupHealthbar();
    }

    private void SetupHealthbar()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        if (playerStats != null)
        {
            Debug.Log("PlayerHealth found, setting up health bar.");
            playerStats.OnHealthChanged.AddListener(UpdateHealthBar);
            playerStats.OnStaminaChanged.AddListener(UpdateStaminaBar);
            UpdateHealthBar();
            UpdateStaminaBar();
        }
        else
        {
            Debug.LogError("PlayerHealth not found! Check if the script is attached.");
        }
    }

    public void UpdateHealthBar()
    {
        if (playerStats != null)
        {
            float normalizedHealth = (float)playerStats.currentHealth / 100f;
            _healthBarForegroundImage.fillAmount = normalizedHealth;
            Debug.Log("Health bar adjusting: " + normalizedHealth);

            StartCoroutine(ShakeHealthBar());
        }
        else
        {
            Debug.LogError("PlayerHealth reference is missing! Health bar won't update.");
        }
    }

    public void UpdateStaminaBar()
    {
        if (playerStats != null)
        {
            float normalizedStamina = (float)playerStats.currentStamina / playerStats.maxStamina;
            _staminaBarForeGroundImage.fillAmount = normalizedStamina;
            Debug.Log("Stamina bar adjusting: " + normalizedStamina);
        }
        else
        {
            Debug.LogError("PlayerStats reference is missing! Stamina bar won't update.");
        }
    }

    private IEnumerator ShakeHealthBar()
    {
        Vector3 originalPos = _healthBarTransform.anchoredPosition;
        float duration = 0.2f;
        float magnitude = 5f; 

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _healthBarTransform.anchoredPosition = originalPos + (Vector3)Random.insideUnitCircle * magnitude;
            yield return null;
        }

        _healthBarTransform.anchoredPosition = originalPos;
    }

    
}
