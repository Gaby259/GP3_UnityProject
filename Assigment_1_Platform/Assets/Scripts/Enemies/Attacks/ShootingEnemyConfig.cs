using UnityEngine;

[CreateAssetMenu(fileName = "ShootingEnemyConfig", menuName = "Enemies/Attack/ShootConfig")]
public class ShootingEnemyConfig : ScriptableObject
{
    public GameObject bulletPrefab;
    public float turnSpeed = 10f;
    public float aimHeight = 1.2f;
    public int burstCount = 3;
    public float burstInterval = 0.12f;
    public float timeForShoot = 1f;
}
