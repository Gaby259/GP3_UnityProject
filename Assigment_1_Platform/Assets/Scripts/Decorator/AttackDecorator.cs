using UnityEngine;

public abstract class AttackDecorator : IIAttack

//abstract class can not make an instance of it 
{
    protected IIAttack wrappedAttack;

    public AttackDecorator(IIAttack attack)
    {
        wrappedAttack = attack;
    }
    public virtual void Execute()
    {
        wrappedAttack.Execute();
    }
}
