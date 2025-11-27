using UnityEngine;

public class PlayerBullet : Projectile
{
    
    private void Update()
    {
        transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        // Damage objects that have the IDamageable script 
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        if (impactParticles != null)
            Instantiate(impactParticles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
