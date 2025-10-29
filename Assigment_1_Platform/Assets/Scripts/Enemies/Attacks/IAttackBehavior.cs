using UnityEngine;

public interface IAttackBehavior
{
    //Tries to perform an attack using the given StateManager context.
    // This is called by the AttackState every frame (usually in FixedUpdate).
    void TryAttack(StateManager stateManager);
}