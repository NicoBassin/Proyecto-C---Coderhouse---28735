using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellTwo : Spells
{
    public event Action OnEnemyHitTwo;
    
    void Awake(){
        FindObjectOfType<PlayerAttack>().OnAttackTwo += Attack;
    }

    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy")){
            OnEnemyHitTwo?.Invoke();
        }
    }
}
