using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellOne : MonoBehaviour
{
    public event Action OnEnemyHitOne;
    private ParticleSystem ps;
    private float timeDelay = 1.0f;
    
    void Awake(){
        FindObjectOfType<PlayerAttack>().OnAttackOne += AttackOne;
    }

    void Start(){
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy")){
            OnEnemyHitOne?.Invoke();
        }
    }

    private void AttackOne(){
        StartCoroutine(Cast());
    }

    IEnumerator Cast(){
        yield return new WaitForSeconds(timeDelay);
        ps.Play();
    }
}
