using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;

    private InputController _inputController;
    private GameObject _shieldEffectInstance;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        _playerHealth = _playerController.GetComponent<PlayerHealth>();
        _inputController = _playerController.GetComponent<InputController>();
        _inputController.ShieldEvent += ActivateShield;
        _inputController.ShieldEvent -= DeactivateShield;
    }

    private void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.ShieldEvent -= ActivateShield;
            _inputController.ShieldEvent += DeactivateShield;
        }
    }

    private void ActivateShield()
    {
        Debug.Log("Shield activated");
        _playerController.SetMovementEnable(false);
        _playerHealth.isInvulnerable = true;

        if (shieldPrefab != null && _shieldEffectInstance == null)
        {
            _shieldEffectInstance = Instantiate(shieldPrefab, _playerController.transform);
            _shieldEffectInstance.transform.localPosition = Vector3.zero;
        }
    }

    private void DeactivateShield()
    {
        Debug.Log("Shield destroyed");
        _playerController.SetMovementEnable(true);
        _playerHealth.isInvulnerable = false;

        if (_shieldEffectInstance != null)
        {
            Destroy(_shieldEffectInstance);
            _shieldEffectInstance = null;
        }
    }
}