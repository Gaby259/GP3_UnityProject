using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Aquí puedes poner animación de muerte, efecto de partículas, etc.
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject);
    }
}