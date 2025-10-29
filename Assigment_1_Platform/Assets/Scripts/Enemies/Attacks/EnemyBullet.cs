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

        if (playerHealth == null)
        {
            DealDamage(playerHealth);
        }

        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, transform.rotation);
        }
   
    }
    private void DealDamage(PlayerHealth health)
    {
        health.TakeDamage(damage);
    }
}
