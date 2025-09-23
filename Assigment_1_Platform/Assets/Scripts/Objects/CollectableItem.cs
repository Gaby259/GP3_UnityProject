using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
//Unity Event visible in Inspector
    public UnityEvent OnItemCollected;
    public UnityEvent<int> OnPoinstAward;

    [SerializeField] private int _pointValue = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Invoke events
            OnItemCollected?.Invoke();
            OnPoinstAward?.Invoke(_pointValue);
            Destroy(gameObject);
        }
    }
}
