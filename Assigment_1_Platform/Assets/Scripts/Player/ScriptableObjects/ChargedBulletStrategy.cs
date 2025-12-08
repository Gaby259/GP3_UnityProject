using UnityEngine;

[CreateAssetMenu(fileName = "ChargedBulletStrategy", menuName = "Player/ShootingStrategy/ChargedBulletStrategy")]
public class ChargedBulletStrategy : ShootingStrategy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int chargedDamage = 2;
    [SerializeField] private float chargedSpeed = 18f;
    [SerializeField] private float scaleMultiplier = 1.5f;
    
    

    public override void Shoot(Transform shootPoint)
    {
        if (!bulletPrefab || !shootPoint) return;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        
        IProjectileConfigurable configurable = bullet.GetComponent<IProjectileConfigurable>();
        if (configurable != null)
        {
            configurable.Configure(chargedDamage, chargedSpeed);
        }
        
        bullet.transform.localScale *= scaleMultiplier;
    }
}