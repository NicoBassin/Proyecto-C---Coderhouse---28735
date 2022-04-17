using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChase : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed = 2.5f;
    [SerializeField] private float rotateSpeed = 2.0f;
    [SerializeField] private float minDistance = 1.0f;
    private bool isAttacking = false;
    private float attackTime = 0.75f;
    private float knockbackStrength = 50.0f;
    private float jumpForceStrength = 3.0f;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<ZombieAttack>().OnAttackPlayer += Attacking;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        LookAtPlayer();
    }

    private void ChasePlayer(){
        if(!isAttacking){
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = currentPosition.y;
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
        }
        else{
            rb.AddRelativeForce(Vector3.back * knockbackStrength);
        }
    }

    private void LookAtPlayer(){
        Vector3 lookPosition = player.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion newRotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    private void Attacking(){
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForceStrength, ForceMode.Impulse);
        StartCoroutine(ChangeState());
    }

    IEnumerator ChangeState(){
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
