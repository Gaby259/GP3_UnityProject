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
        
        Vector3 playerXZ = stateManager.PlayerChecker.GetPlayerPosition();
        Vector3 targetPosition = new Vector3(playerXZ.x, transform.position.y, playerXZ.z);

        // Usa el rango del PlayerChecker (equivalente a tu detectionDistance)
        _canShoot = stateManager.PlayerChecker.IsPlayerInRange();

        // Rotación suave hacia el jugador (RotateTowards + LookRotation)
        Transform rotationReference = (aimPoint != null) ? aimPoint : transform;
      //  Vector3 lookDirection = targetPosition - rotationReference.position;
        float enemyRotation = config.turnSpeed * Time.deltaTime;
        Vector3 newLookDirection = Vector3.RotateTowards(rotationReference.forward, targetPosition, enemyRotation, 0f);
        rotationReference.rotation = Quaternion.LookRotation(newLookDirection); // actualiza la rotación
        

        // Si no está en rango de ataque, no dispares 
        if (!_canShoot) return;

        // Cooldown entre ráfagas 
        if (Time.time < _timeSinceLastShot + Mathf.Max(0.01f, config.timeForShoot)) return;

        // Disparo en ráfaga 
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

        Vector3 target = stateManager.PlayerChecker.GetPlayerPosition() + Vector3.up * config.aimHeight;
        Vector3 dir = target - shootingPoint.position;
        Debug.Log(shootingPoint.position);
        Instantiate(config.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
}

