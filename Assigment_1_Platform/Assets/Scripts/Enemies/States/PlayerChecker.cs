using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private Transform playerTransform;

    public bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance <= checkRadius;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerTransform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}