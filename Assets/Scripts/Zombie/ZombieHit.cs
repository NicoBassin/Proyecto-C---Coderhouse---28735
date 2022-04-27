using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieHit : MonoBehaviour
{
    public event Action <GameObject> OnEnemyKilled;
    private int zombieLife = 20;
    Rigidbody rb;
    private void Awake(){
        rb = GetComponent<Rigidbody>();
        foreach(Spells spell in FindObjectsOfType<Spells>()){
            spell.OnEnemyHit += OnZombieHit;
        }
    }

    private void OnZombieHit(int damage, int spellForce, GameObject zombie){
        if(this.gameObject == zombie){
            zombieLife -= damage;
            if(zombieLife > 0){
                rb.AddRelativeForce(Vector3.back * spellForce, ForceMode.Impulse);
            }
            else{
                StartCoroutine(DeleteZombie());
            }
        }
    }

    IEnumerator DeleteZombie(){
        foreach(Spells spell in FindObjectsOfType<Spells>()){
            spell.OnEnemyHit -= OnZombieHit;
        }
        OnEnemyKilled?.Invoke(this.gameObject);
        GameManager.gmInstance.score++;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
