using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTwo : Spells
{
    public override int Damage { get{return 30;}}
    public override int SpellForce { get{return 45;}}
    
    protected override void Awake(){
        base.Awake();
        FindObjectOfType<PlayerAttack>().OnAttackTwo += Attack;
    }
}
