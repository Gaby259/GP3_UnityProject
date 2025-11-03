using UnityEngine;
using UnityEngine.AI;
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    
    private Vector3 _targetPosition;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void ImplementConfig(ChaserEnemyConfig chaserConfig)
    {
        agent.speed = chaserConfig.moveSpeed;
        agent.stoppingDistance = chaserConfig.stoppingDistance;
        agent.angularSpeed = chaserConfig.turnSpeed;
    }
    public void MoveTo(Vector3 targetTransform)
    {
        _targetPosition = targetTransform;
        agent.SetDestination(_targetPosition);
    }

}