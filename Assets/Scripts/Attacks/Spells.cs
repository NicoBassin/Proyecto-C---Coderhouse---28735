using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    private ParticleSystem ps;
    protected float castDelay = 0.7f;

    void Start(){
        ps = GetComponent<ParticleSystem>();
    }

    protected void Attack(){
        StartCoroutine(Cast());
    }

    IEnumerator Cast(){
        yield return new WaitForSeconds(castDelay);
        ps.Play();
    }
}
