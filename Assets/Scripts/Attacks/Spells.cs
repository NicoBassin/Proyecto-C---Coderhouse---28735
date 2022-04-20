using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spells : MonoBehaviour
{
    public event Action <int, int, GameObject> OnEnemyHit;
    protected float castDelay = 0.7f;
    private int damage, spellForce;
    public virtual int Damage{get {return damage;}}
    public virtual int SpellForce{get {return spellForce;}}
    private ParticleSystem ps;

    protected virtual void Awake(){
        ps = GetComponent<ParticleSystem>();
    }

    protected void Attack(){
        StartCoroutine(Cast());
    }

    IEnumerator Cast(){
        yield return new WaitForSeconds(castDelay);
        ps.Play();
    }

    protected void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy")){
            OnEnemyHit?.Invoke(Damage, SpellForce, other.gameObject);
        }
    }
}
