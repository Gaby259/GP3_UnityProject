using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyHurtbox : MonoBehaviour
{
    [Header("Damage Settings")] 
    [SerializeField] private int damagePerHit = 1;
    [SerializeField] private float damageCooldown = 1.0f;
    [SerializeField] private bool dealDamageOnEnter = true;
    [SerializeField] private string playerTag = "Player";
    
    private float _nextDamageTime = 0.0f;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        Collider hurtboxCollider = GetComponent<Collider>();
        hurtboxCollider.isTrigger = true;

        enabled = false; // The hurtbox starts deactivated, the attack will activate it when is necessarily 
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (_playerHealth == null)
        {
            return;
        }
        
        if (other.CompareTag(playerTag) && dealDamageOnEnter)
        {
            ApplyDamage(_playerHealth);
            _nextDamageTime = Time.time + damageCooldown;
        }
        else
        {
            _nextDamageTime =  Time.time + damageCooldown;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_playerHealth == null)
            return;

        if (!other.CompareTag(playerTag))
            return;

        if (Time.time < _nextDamageTime)
            return;

        ApplyDamage(_playerHealth);
        _nextDamageTime = Time.time + damageCooldown;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        // Resets Time and the player reference
        _nextDamageTime = 0f;
        _playerHealth = null;
    }
    private void ApplyDamage(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(damagePerHit);
        //Sound effects can be added here
    }
}
