using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    private PlayerStamina _stamina;

    private void Start()
    {
        _stamina = FindFirstObjectByType<PlayerStamina>();
        staminaSlider.maxValue = _stamina.MaxStamina;
        staminaSlider.value = _stamina.CurrentStamina;

        //stamina subscribes to the function
        _stamina.OnStaminaChanged += UpdateStaminaUI;
    }

    private void UpdateStaminaUI(float current, float max)
    {
        staminaSlider.maxValue = max;
        staminaSlider.value = current;
    }
}