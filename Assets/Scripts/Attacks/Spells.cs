using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spells : MonoBehaviour
{
    public event Action OnEnemyHitOne;
    public event Action OnEnemyHitTwo;
    private ParticleSystem ps;
    private float castDelayOne = 0.667f;
    private float castDelayTwo = 0.762f;
    private bool spellOne = false;
    private bool spellTwo = false;
    private float spellOneSize = 2f;
    private float spellTwoSize = 3.5f;
    Queue SpellQueue = new Queue();
    ParticleSystem.MainModule main;
    
    void Awake(){
        FindObjectOfType<PlayerAttack>().OnAttackOne += AttackOne;
        FindObjectOfType<PlayerAttack>().OnAttackTwo += AttackTwo;
    }

    void Start(){
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }

    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(spellOne){
                OnEnemyHitOne?.Invoke();
            }
            if(spellTwo){
                OnEnemyHitTwo?.Invoke();
            }
        }
    }

    private void AttackOne(){
        StartCoroutine(CastOne());
    }

    private void AttackTwo(){
        StartCoroutine(CastTwo());
    }

    IEnumerator CastOne(){
        yield return new WaitForSeconds(castDelayOne);
        main.startSize = spellOneSize;
        main.startColor = Color.red;
        ps.Play();
    }

    IEnumerator CastTwo(){
        yield return new WaitForSeconds(castDelayTwo);
        main.startSize = spellTwoSize;
        main.startColor = Color.magenta;
        ps.Play();
    }
}
