using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOne : Spells
{
    public override int Damage { get{return 10;}}
    public override int SpellForce { get{return 15;}}
    
    protected override void Awake(){
        base.Awake();
        FindObjectOfType<PlayerAttack>().OnAttackOne += Attack;
    }
}
