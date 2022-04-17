using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieAttack : MonoBehaviour
{
    public event Action OnAttackPlayer;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            OnAttackPlayer?.Invoke();
        }
    }
}
