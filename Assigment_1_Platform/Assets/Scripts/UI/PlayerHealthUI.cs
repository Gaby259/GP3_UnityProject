using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage; // tu marca libros morado
    private PlayerHealth _health;

    private void Start()
    {
        _health = FindObjectOfType<PlayerHealth>();

        // Subscribirse al evento
        _health.OnHealthChanged += UpdateHealthUI;

        // Set inicial
        UpdateHealthUI(_health.CurrentHealth, _health.MaxHealth);
    }

    private void UpdateHealthUI(int current, int max)
    {
        float amount = (float)current / max;
        healthFillImage.fillAmount = amount;
    }
}