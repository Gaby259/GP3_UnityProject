using UnityEngine;

[CreateAssetMenu(fileName = "ShootingEnemyConfig", menuName = "Enemies/Attack/ShootConfig")]
public class ShootingEnemyConfig : ScriptableObject
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float turnSpeed = 10f;
    public float aimHeight = 1.2f;
    
    [Header("Burst Settings")]
    public int burstCount = 3;
    public float burstInterval = 0.12f;
    
    [Header("Cooldown Settings")]
    public float timeForShoot = 1f; //Time between waves
}
