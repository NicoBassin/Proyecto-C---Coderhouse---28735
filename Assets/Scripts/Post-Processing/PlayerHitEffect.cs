using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    [SerializeField] private GameObject ppVolume;
    private float delayTime = 0.2f;
    private void Awake() {
        foreach(ZombieAttack zombie in FindObjectsOfType<ZombieAttack>()){
            zombie.OnAttackPlayer += AttackVFX;
        }
    }

    private void AttackVFX(int damage, GameObject zombie){
        StartCoroutine(VFX());
    }

    IEnumerator VFX(){
        ppVolume.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        ppVolume.SetActive(false);
    }
}
