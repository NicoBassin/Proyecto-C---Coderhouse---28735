using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    public event Action <int, GameObject> OnAttackPlayer;

    protected void CallEvent(int damage, GameObject zombie){
        OnAttackPlayer?.Invoke(damage, zombie);
    }
}
