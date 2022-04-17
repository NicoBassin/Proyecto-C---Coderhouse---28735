using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAttackOne;
    public event Action OnAttackTwo;
    private GameObject player;
    private bool hasWeapon = true;
    private float attackOneCooldown = 2.0f;
    private float attackTwoCooldown = 5.0f;
    private bool canAttackOne = true;
    private bool canAttackTwo = true;
    private float timePassOne = 0f;
    private float timePassTwo = 0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        Timers();

        if (Input.GetKey(KeyCode.Mouse0) && canAttackOne)
        {
            OnAttackOne?.Invoke();
            canAttackOne = false;
        }
        if (Input.GetKey(KeyCode.Mouse1) && canAttackTwo)
        {
            OnAttackTwo?.Invoke();
            canAttackTwo = false;
        }
    }

    private void Timers()
    {
        if (!canAttackOne)
        {
            if (timePassOne < attackOneCooldown)
            {
                timePassOne += Time.deltaTime;
            }
            else
            {
                canAttackOne = true;
                timePassOne = 0f;
            }
        }
        if (!canAttackTwo)
        {
            if (timePassTwo < attackTwoCooldown)
            {
                timePassTwo += Time.deltaTime;
            }
            else
            {
                canAttackTwo = true;
                timePassTwo = 0f;
            }
        }
    }
}
