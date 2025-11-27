using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootPoint;

    [Header("Strategy Config")]
    [SerializeField] private ShootingStrategy defaultShootingStrategy;
    
    private ShootingStrategy _currentShootingStrategy;
    private InputController _inputController;

    public event Action<string> OnShootingStrategyChanged;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    private void OnEnable()
    {
        if (_inputController != null)
            _inputController.AttackEvent += HandleShootInput;
    }

    private void OnDisable()
    {
        if (_inputController != null)
            _inputController.AttackEvent -= HandleShootInput;
    }

    private void Start()
    {
        if (defaultShootingStrategy != null)
            SetShootingStrategy(defaultShootingStrategy);
    }

    private void HandleShootInput()
    {
        Debug.Log("Shooting");
        _currentShootingStrategy?.Shoot(shootPoint);
    }
    public void SetShootingStrategy(ShootingStrategy newStrategy)
    {
        if (newStrategy == null) return;

        _currentShootingStrategy = newStrategy;
        OnShootingStrategyChanged?.Invoke(_currentShootingStrategy.ShootingStrategyName);
    }
}