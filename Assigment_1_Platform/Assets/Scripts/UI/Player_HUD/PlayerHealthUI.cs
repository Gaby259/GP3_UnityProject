using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage; 
    private PlayerHealth _health;
    
    private void Start()
    {
        _health = FindFirstObjectByType<PlayerHealth>();
        _health.OnHealthChanged += UpdateHealthUI;
        UpdateHealthUI(_health.CurrentHealth, _health.MaxHealth);
    }

    private void UpdateHealthUI(int current, int max)
    {
        float amount = (float)current / max;
        healthFillImage.fillAmount = amount;
    }

   
}