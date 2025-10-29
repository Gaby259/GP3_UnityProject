using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 3f;
    [SerializeField] protected ParticleSystem impactParticles;
    [SerializeField] protected int damage = 1;

    private void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }
}
