using UnityEngine;

public class EnemyBullet : Projectile
{
    private void Update()
    {  
        transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth)
        {
            DealDamage(playerHealth);
            Destroy(gameObject);
        }
        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    private void DealDamage(PlayerHealth health)
    {
        health.TakeDamage(damage);
    }
}
