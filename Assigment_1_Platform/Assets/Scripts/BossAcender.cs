using UnityEngine;
using UnityEngine.Events;

public class BossAcender : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform startPoint;  // punto inicial
    [SerializeField] private Transform endPoint;    // punto final
    [SerializeField] private float _speed = 0f;     // velocidad actual (asignada por LavaDirector)

    [Header("Events")]
    public UnityEvent OnLavaStarted;
    public UnityEvent OnLavaStopped;
    public UnityEvent<float> OnSpeedChanged;
    public UnityEvent OnPlayerCaught;
    
    [Header("Damage")]
    [SerializeField] private int damagePerHit = 1;      // how much HP per tick
    [SerializeField] private float cooldownSeconds = 3f;// delay between hits while inside
    [SerializeField] private bool hitOnEnter = true;

    private bool _active = false;
    private float _nextHitTime = 0f;       

    private void Awake()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;

        // Si tenemos startPoint, arrancamos desde allí
        if (startPoint != null)
            transform.position = startPoint.position;

        // Start from startPoint, if any
        if (startPoint != null)
            transform.position = startPoint.position;
    }

    private void Update()
    {
        if (_active)
        {

            // Mueve la lava hacia el endPoint
            transform.position = Vector3.MoveTowards(
                transform.position,
                endPoint.position,
                _speed * Time.deltaTime
            );

            // Si llegó al final, se detiene
            if (Vector3.Distance(transform.position, endPoint.position) < 0.01f)
                Stop();
        }
    }

    public void Begin()
    {
        if (_active)
        {
            return;
        }
        _active = true;
        OnLavaStarted?.Invoke();
    }

    public void Stop()
    {
        if (!_active)
        {
            return;
        }
        _active = false;
        SetSpeed(0);
        OnLavaStopped?.Invoke();
    }

    public void SetSpeed(float s)
    {
        _speed = Mathf.Max(0f, s);
        OnSpeedChanged?.Invoke(_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        

        var health = other.GetComponentInParent<PlayerHealth>();
        if (health == null) return;

        if (hitOnEnter)
        {
            DealDamage(health);                          // first hit on enter
            _nextHitTime = Time.time + cooldownSeconds;  // wait for next tick
        }
        else
        {
            _nextHitTime = Time.time + cooldownSeconds;  // first hit after cooldown
        }

        OnPlayerCaught?.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (Time.time < _nextHitTime) return;

        var health = other.GetComponentInParent<PlayerHealth>();
        if (health == null) return;

        DealDamage(health);
        _nextHitTime = Time.time + cooldownSeconds;      // schedule next tick
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        // Reset timer so a re-entry can hit immediately (if hitOnEnter) or after cooldown
        _nextHitTime = 0f;
    }

    private void DealDamage(PlayerHealth health)
    {
        health.TakeDamage(damagePerHit);
    }
}
