using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    [SerializeField] private LavaManager lavaManager;

    private bool hasActivated = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return; 

        if (other.CompareTag("Player"))
        {
            lavaManager.StartPhases();
            hasActivated = true;
            Debug.Log("Lava triggered ONCE");
        }
    }
}