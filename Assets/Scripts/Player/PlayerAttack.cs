using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAttackOne;
    public event Action OnAttackTwo;
    private GameObject player;
    private float attackOneCooldown = 1.0f;
    private float attackTwoCooldown = 3.0f;
    private bool isAttackingOne = false;
    private bool isAttackingTwo = false;
    private bool canAttackOne = true;
    private bool canAttackTwo = true;
    private float timePassOne = 0f;
    private float timePassTwo = 0f;
    private float animationTimeOne = 1.84f;
    private float animationTimeTwo = 1.5f;


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
        SpellsTimers();

        if (Input.GetKey(KeyCode.Mouse0) && canAttackOne && !isAttackingOne && !isAttackingTwo)
        {
            OnAttackOne?.Invoke();
            StartCoroutine(AttackingOne());
            canAttackOne = false;
        }
        if (Input.GetKey(KeyCode.Mouse1) && canAttackTwo && !isAttackingOne && !isAttackingTwo)
        {
            OnAttackTwo?.Invoke();
            StartCoroutine(AttackingTwo());
            canAttackTwo = false;
        }
    }

    private void SpellsTimers()
    {
        if (!canAttackOne && !isAttackingOne)
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
        if (!canAttackTwo && !isAttackingTwo)
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

    IEnumerator AttackingOne(){
        isAttackingOne = true;
        yield return new WaitForSeconds(animationTimeOne);
        isAttackingOne = false;
    }

    IEnumerator AttackingTwo(){
        isAttackingTwo = true;
        yield return new WaitForSeconds(animationTimeTwo);
        isAttackingTwo = false;
    }
}
