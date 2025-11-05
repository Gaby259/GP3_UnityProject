using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootPoint;

    [Header("Strategy Config")]
    [SerializeField] private ShootingStrategy defaultShootingStrategy;
    
    [Header("Fire Rate")]
    [SerializeField] private float shotsPerSecond = 8f; // 8 = una bala cada 0.125 s
    private float _nextShotTime = 0f;
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
        _currentShootingStrategy?.Shoot(shootPoint);
    }
    public void SetShootingStrategy(ShootingStrategy newStrategy)
    {
        if (newStrategy == null) return;

        _currentShootingStrategy = newStrategy;
        Debug.Log($"Shooting strategy changed to {_currentShootingStrategy.ShootingStrategyName}");
        OnShootingStrategyChanged?.Invoke(_currentShootingStrategy.ShootingStrategyName);
    }
}