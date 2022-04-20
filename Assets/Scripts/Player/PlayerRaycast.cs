using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRaycast : MonoBehaviour
{
    public event Action <bool, GameObject> OnEnemyRaycasted;
    [SerializeField] private GameObject shootPoint;
    private float raySpellDistance = 15f;
    private GameObject lastHit;
    private bool deleted = false;

    private void Awake(){
        foreach(ZombieHit zombie in FindObjectsOfType<ZombieHit>()){
            zombie.OnEnemyKilled += Deleted;
        }
    }

    private void Update(){
        PlayerRaycastSpells();
    }

    private void PlayerRaycastSpells(){
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, raySpellDistance)){
            if(hit.transform.CompareTag("Enemy")){
                OnEnemyRaycasted?.Invoke(true, hit.transform.gameObject);
            }
            lastHit = hit.transform.gameObject;
        }
        else{
            if(lastHit != null && !deleted){
                OnEnemyRaycasted?.Invoke(false, lastHit);
            }
        }
    }

    private void Deleted(GameObject zombie){
        StartCoroutine(DeletedCoroutine());
    }

    IEnumerator DeletedCoroutine(){
        deleted = true;
        yield return new WaitForSeconds(0.2f);
    }
}
