using UnityEngine;

public class Projectile : MonoBehaviour, IProjectileConfigurable
{
    [SerializeField] protected float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 3f;
    [SerializeField] protected ParticleSystem impactParticles;
    [SerializeField] protected int damage = 1;

    private void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }

    public void Configure(int newDamage, float newSpeed)
    {
        damage = newDamage;
        projectileSpeed = newSpeed;
    }
}
