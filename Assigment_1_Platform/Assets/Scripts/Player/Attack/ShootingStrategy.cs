using UnityEngine;

[CreateAssetMenu(fileName = "ShootingStrategy", menuName = "Player/ShootingStrategy")]
public abstract class ShootingStrategy : ScriptableObject
{
    public string ShootingStrategyName = "Default";
    public abstract void Shoot(Transform shootPoint);
}
