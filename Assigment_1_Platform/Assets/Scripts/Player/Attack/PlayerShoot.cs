using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerStamina stamina;

    [Header("Strategy Config")]
    [SerializeField] private ShootingStrategy defaultShootingStrategy;
    [SerializeField] private ShootingStrategy chargedStrategy;
    
    [Header("Charge Settings")]
    [SerializeField] private float minChargeTime = 0.4f;
    [SerializeField] private float staminaDrainPerSecond = 12f;
    
    
    private bool _isCharging;
    private float _chargeTimer;
    
    private ShootingStrategy _currentShootingStrategy;
    private InputController _inputController;

    public event Action<string> OnShootingStrategyChanged;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        stamina =  stamina.GetComponent<PlayerStamina>();
    }

    private void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.StartAttackEvent += StartCharge;
            _inputController.EndAttackEvent += ReleaseShot;
        }
          
    }

    private void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.StartAttackEvent -= StartCharge;
            _inputController.EndAttackEvent -= ReleaseShot;
        }
    }

    private void Start()
    {
        if (defaultShootingStrategy != null)
            SetShootingStrategy(defaultShootingStrategy);
    }

    private void Update()
    {
        if (_isCharging)
        {
            UpdateCharging();
        }
    }

    private void StartCharge()
    {
        if (stamina.CurrentStamina <= 0) return;    
        _isCharging = true;
        _chargeTimer = 0;
    }

    private void UpdateCharging()
    {
        _chargeTimer += Time.deltaTime;
        
        stamina.ReduceStamina(staminaDrainPerSecond * Time.deltaTime);
        if (stamina.CurrentStamina <= 0)
        {
            _isCharging = false;
            SetShootingStrategy(defaultShootingStrategy);
            _currentShootingStrategy.Shoot(shootPoint);
        }
    }
    
    private void ReleaseShot()
    {
        if (!_isCharging) return;

        _isCharging = false;

        if (_chargeTimer >= minChargeTime) { SetShootingStrategy(chargedStrategy); }
        
        else { SetShootingStrategy(defaultShootingStrategy); }

        _currentShootingStrategy.Shoot(shootPoint);
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