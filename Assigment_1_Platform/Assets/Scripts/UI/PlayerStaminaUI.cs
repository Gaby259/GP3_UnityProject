using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    private PlayerStamina _stamina;

    private void Start()
    {
        _stamina = FindObjectOfType<PlayerStamina>();

        // Setup inicial
        staminaSlider.maxValue = _stamina.MaxStamina;
        staminaSlider.value = _stamina.CurrentStamina;

        // Evento
        _stamina.OnStaminaChanged += UpdateStaminaUI;
    }

    private void UpdateStaminaUI(float current, float max)
    {
        staminaSlider.maxValue = max;
        staminaSlider.value = current;
    }
}