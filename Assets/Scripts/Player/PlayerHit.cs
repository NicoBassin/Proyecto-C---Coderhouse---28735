using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void Awake()
    {
        foreach(ZombieAttack zombie in FindObjectsOfType<ZombieAttack>()){
            zombie.OnAttackPlayer += Attacked;
        }
    }

    private void Attacked(int enemyDamage, GameObject zombie){
        GameManager.gmInstance.playerLife -= enemyDamage;
        if(GameManager.gmInstance.playerLife <= 0){
            HUDController.HUDMInstance.NextLevel();
        }
    }
}
