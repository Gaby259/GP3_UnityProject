using UnityEngine;

public class PoisonAttackDecorator : AttackDecorator
{
    public PoisonAttackDecorator(IIAttack attack) : base(attack){ }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("Poison!!!");
    }
}
