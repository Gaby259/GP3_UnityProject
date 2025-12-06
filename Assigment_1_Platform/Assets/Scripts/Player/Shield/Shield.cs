using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;

    private InputController _inputController;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    private GameObject _shieldInstance;

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        _playerHealth = _playerController.GetComponent<PlayerHealth>();
        _inputController = _playerController.GetComponent<InputController>();

        _inputController.ShieldActivateEvent += ActivateShield;
        _inputController.ShieldDeactivateEvent += DeactivateShield;
    }

    private void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.ShieldActivateEvent -= ActivateShield;
            _inputController.ShieldDeactivateEvent -= DeactivateShield;
        }
    }

    private void ActivateShield()
    {

        if (_shieldInstance == null)
        {
            _shieldInstance = Instantiate(shieldPrefab, _playerController.transform);
            _shieldInstance.transform.localPosition = Vector3.zero;
        }
        else
        {
            _shieldInstance.SetActive(true);
            SoundManager.PlaySFX("Shield");
        }

        _playerController.SetMovementEnable(false);
        _playerHealth.isInvulnerable = true;
    }

    private void DeactivateShield()
    {

        if (_shieldInstance != null)
            _shieldInstance.SetActive(false);

        _playerController.SetMovementEnable(true);
        _playerHealth.isInvulnerable = false;
    }
}