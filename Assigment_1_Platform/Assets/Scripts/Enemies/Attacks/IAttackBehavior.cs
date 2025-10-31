using UnityEngine;

public interface IAttackBehavior
{
    //Tries to perform an attack using the given StateManager context.
    void TryAttack(StateManager stateManager);
}