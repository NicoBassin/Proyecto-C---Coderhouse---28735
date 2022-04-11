using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChase : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float chaseSpeed = 2.5f;
    [SerializeField] private float rotateSpeed = 2.0f;
    [SerializeField] private float minDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        LookAtPlayer();
    }

    private void ChasePlayer(){
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        Vector3 targetPosition = player.transform.position;
        targetPosition.y = 0;
        if((targetPosition-currentPosition).magnitude <= minDistance){
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
        }
    }

    private void LookAtPlayer(){
        Vector3 lookPosition = player.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion newRotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }
}
