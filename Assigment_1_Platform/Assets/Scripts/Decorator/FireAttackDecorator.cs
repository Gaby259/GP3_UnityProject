using UnityEngine;

public class FireAttackDecorator: AttackDecorator
{
   public FireAttackDecorator(IIAttack attack) : base(attack) {}// the fire attack will go though the base attack (wrapped) and update the base attack.  
 
   public override void Execute()
   {
      base.Execute(); //we want the original to execute
      Debug.Log("Fire!!!");
   }
   
}
