using UnityEngine;

[CreateAssetMenu(fileName = "BulletStrategy", menuName = "Player/ShootingStrategy/BulletStrategy")]
public class BulletStrategy : ShootingStrategy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 6f;
    private float _nextAllowedFireTime;

   public override void Shoot(Transform shootPoint)
{
    if (!bulletPrefab || !shootPoint) return;
    
    if (Time.time < _nextAllowedFireTime)
        return;
    
    _nextAllowedFireTime = Time.time + (1f / fireRate);
    Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    SoundManager.PlaySFX("Attack");
}

}
