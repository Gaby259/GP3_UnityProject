using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public event System.Action<float, float> OnStaminaChanged;
    
    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float regenRate = 10f; 
    [SerializeField] private float regenDelay = 1f; 

    [Header("Healing Settings")]
    [SerializeField] private float healStaminaCost = 25f;
    [SerializeField] private int healAmount = 1;

    private float _currentStamina;
    private Coroutine regenCoroutine;
    private Coroutine delayCoroutine;

    public float CurrentStamina => _currentStamina;
    public float MaxStamina => maxStamina;

    private void Awake()
    {
        _currentStamina = maxStamina;
    }

    private void Start()
    {
        OnStaminaChanged?.Invoke(_currentStamina, maxStamina);
    }
    
    private void StartRegenWithDelay()
    {
        // Start regenerate the stamina BUT player cant use it if the delay is not complete
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }

        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }

        delayCoroutine = StartCoroutine(RegenDelay());
    }

    private IEnumerator RegenDelay()
    {
        yield return new WaitForSeconds(regenDelay);
        regenCoroutine = StartCoroutine(RegenerateHealth());
        delayCoroutine = null;
    }

    private IEnumerator RegenerateHealth()
    {
        while (_currentStamina < maxStamina)
        {
            _currentStamina += regenRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
            OnStaminaChanged?.Invoke(_currentStamina, maxStamina);

            yield return null;
        }

        regenCoroutine = null;
    }
    
    private bool SpendStamina(float cost)
    {
        if (_currentStamina < cost)
            return false;

        _currentStamina -= cost;
        OnStaminaChanged?.Invoke(_currentStamina, maxStamina);
        StartRegenWithDelay();

        return true;
    }
    public void ReduceStamina(float amount)
    {
        _currentStamina -= amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);

        OnStaminaChanged?.Invoke(_currentStamina, maxStamina);

        StartRegenWithDelay();
    }
    public bool Heal(PlayerHealth healthScript)
    {
        if (!SpendStamina(healStaminaCost))
            return false;

        healthScript.Heal(healAmount);
        return true;
    }
}
