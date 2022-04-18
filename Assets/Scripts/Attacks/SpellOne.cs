using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellOne : Spells
{
    public event Action OnEnemyHitOne;

    void Awake(){
        FindObjectOfType<PlayerAttack>().OnAttackOne += Attack;
    }

    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy")){
            OnEnemyHitOne?.Invoke();
        }
    }
}
