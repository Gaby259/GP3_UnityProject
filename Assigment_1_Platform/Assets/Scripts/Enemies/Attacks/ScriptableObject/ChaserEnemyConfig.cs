using UnityEngine;

[CreateAssetMenu(fileName = "ChaserEnemyConfig", menuName = "Enemies/Attack/ChaserEnemyConfig")]
public class ChaserEnemyConfig : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    public float turnSpeed = 10f;
    
    [Header("Attack")]
    public float attackRadius = 1.2f;
    public float attackCooldown = 1f;
    public float windupTime = 0.3f;
    public float chargeDuration = 0.35f;
    public float chargeSpeedMultiplier = 2f;
    
}
