using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieAttack : EnemyAttack
{
    private int zombieDamage = 10;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            CallEvent(zombieDamage, this.gameObject);
        }
    }
}
