using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMover))]
public class MeleeBehavior : MonoBehaviour, IAttackBehavior
{
    [Header("Configuration")]
    [SerializeField] private ChaserEnemyConfig chaseConfig;
    
    [Header("References")]
    [SerializeField] private EnemyHurtbox  hurtbox; 
    
    private NavMeshAgent _agent;
    private CharacterMover _characterMover;
    private bool _isAttacking;
    private float _nextAttackTime =0f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _characterMover = GetComponent<CharacterMover>();
    }
    
    void Start()
    {
        _characterMover.ImplementConfig(chaseConfig); //Applies the config made in the scriptable object 
        if (hurtbox != null)
        {
            hurtbox.enabled = false;
        }
    }

    public void TryAttack (StateManager stateManager)
    {
        if (_isAttacking) return;
        if (Time.time < _nextAttackTime) return;
        
        //Player distance
        Vector3 playerPosition = stateManager.PlayerChecker.GetPlayerPosition();
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        
        //If player is near attack
        float requiredDisrance = Mathf.Max(chaseConfig.attackRadius * 1.5f, _agent.stoppingDistance +0.2f);
        if (distanceToPlayer > requiredDisrance) return;

        StartCoroutine(DoChargeAttack(stateManager));
    }

    private IEnumerator DoChargeAttack(StateManager stateManager)
    {
        _isAttacking = true;
        float elapsedWindup = 0f;
        while (elapsedWindup < chaseConfig.windupTime)
        {
            FaceTargetXZ(stateManager.PlayerChecker.GetPlayerPosition(), chaseConfig.turnSpeed);
            elapsedWindup += Time.deltaTime;
            yield return null;
        }

        // 2️⃣ Activar la hurtbox durante la ventana de ataque
        if (hurtbox != null)
            hurtbox.enabled = true;

        // 3️⃣ Cargar hacia el jugador
        float originalSpeed = _agent.speed;
        _agent.speed = originalSpeed * chaseConfig.chargeSpeedMultiplier;

        Vector3 targetPosition = stateManager.PlayerChecker.GetPlayerPosition();
        _agent.SetDestination(targetPosition);

        yield return new WaitForSeconds(chaseConfig.chargeDuration);

        // 4️⃣ Desactivar hurtbox y resetear estados
        if (hurtbox != null)
            hurtbox.enabled = false;

        _agent.speed = chaseConfig.moveSpeed;
        _nextAttackTime = Time.time + chaseConfig.attackCooldown;
        _isAttacking = false;
    }

    private void FaceTargetXZ(Vector3 targetWorldPosition, float degreesPerSecond)
    {
        Vector3 directionToTarget = targetWorldPosition - transform.position;
        directionToTarget.y = 0f;

        if (directionToTarget.sqrMagnitude < 0.0001f)
            return;

        float maxRadians = Mathf.Deg2Rad * degreesPerSecond * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget.normalized, maxRadians, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    }
    
}
