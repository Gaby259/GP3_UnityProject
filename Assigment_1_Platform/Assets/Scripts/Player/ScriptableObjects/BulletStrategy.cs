using UnityEngine;

[CreateAssetMenu(fileName = "BulletStrategy", menuName = "Player/ShootingStrategy/BulletStrategy")]
public class BulletStrategy : ShootingStrategy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 6f;
    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private float projectileSpeed = 20f;
    private float _nextAllowedFireTime;

   public override void Shoot(Transform shootPoint)
{
    if (!bulletPrefab || !shootPoint) return;
    
    if (Time.time < _nextAllowedFireTime)
        return;
    
    _nextAllowedFireTime = Time.time + (1f / fireRate);
    GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    IProjectileConfigurable configurable = bullet.GetComponent<IProjectileConfigurable>();
    if (configurable != null)
    {
        configurable.Configure(projectileDamage, projectileSpeed);
    }

    SoundManager.PlaySFX("Attack");
}

}
