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

    private bool _active = false;

    private void Awake()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;

        // Si tenemos startPoint, arrancamos desde allí
        if (startPoint != null)
            transform.position = startPoint.position;
    }

    private void Update()
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

    public void Begin()
    {
        if (_active) return;
        _active = true;
        OnLavaStarted?.Invoke();
    }

    public void Stop()
    {
        if (!_active) return;
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
        if (!other.CompareTag("Player")) return;

      Debug.Log("Player caught");
        OnPlayerCaught?.Invoke();
        
    }
}
