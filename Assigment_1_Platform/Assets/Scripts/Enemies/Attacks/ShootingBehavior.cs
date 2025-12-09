using System.Collections;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour, IAttackBehavior
{

    [Header("Shooting Config")]
    [SerializeField] private ShootingEnemyConfig config;
    
    [Header("Reference")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform aimPoint;
    
    private float _timeSinceLastShot = 2;
    private Coroutine _burstRoutine; 
    private bool _canShoot= false;
    
    public void TryAttack(StateManager stateManager)
    {
        if (config == null || stateManager == null || stateManager.PlayerChecker == null) return; //Null checks
        if (shootingPoint == null) return;
        
        Vector3 playerPosition = stateManager.PlayerChecker.GetPlayerPosition();
        Vector3 targetPosition = playerPosition - transform.position;
        targetPosition.y = 0f; // no Y leaning 

        //Use Player checker
        _canShoot = stateManager.PlayerChecker.IsPlayerInRange();

        //Rotate Towards Player
        float enemyRotation = config.turnSpeed * Time.deltaTime;
        Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, targetPosition.normalized, enemyRotation, 0f);
        transform.rotation = Quaternion.LookRotation(newLookDirection, Vector3.up);
           

        // if player is not in range dont shoot
        if (!_canShoot) return;

        // Cooldown between bursts
        if (Time.time < _timeSinceLastShot + Mathf.Max(0.01f, config.timeForShoot)) return;

        // Burst routine
        _timeSinceLastShot = Time.time;
        if (_burstRoutine != null) StopCoroutine(_burstRoutine);
        _burstRoutine = StartCoroutine(FireBurst(stateManager));
    }

    private IEnumerator FireBurst(StateManager stateManager)
    {
        for (int i = 0; i < config.burstCount; i++)
        {
            Fire(stateManager);
            yield return new WaitForSeconds(config.burstInterval);
        }
    }

    private void Fire(StateManager stateManager)
    {
        if (config == null || config.bulletPrefab == null || shootingPoint == null || stateManager == null || stateManager.PlayerChecker == null)
            return;
        Instantiate(config.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        SoundManager.PlaySFX("Enemy Shoot");
    }
}

