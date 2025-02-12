using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarForegroundImage;
    [SerializeField] private RectTransform _healthBarTransform;
    private PlayerCombat playerCombat;

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        if (playerCombat != null)
        {
            playerCombat.OnHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar();
        }
    }

    public void UpdateHealthBar()
    {
        if (playerCombat != null)
        {
            float normalizedHealth = (float)playerCombat.playerHealth / 100f;
            _healthBarForegroundImage.fillAmount = normalizedHealth;
            Debug.Log("Health bar adjusting: " + normalizedHealth);

            StartCoroutine(ShakeHealthBar());
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
