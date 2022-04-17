using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWaypointsIA : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float stayTime = 5.0f;
    [SerializeField] private float holdRotationTime = 4.0f;

    private int index = 0;
    private bool moving = true;
    private bool rotating = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsWaypoint();
    }
    
    private void MoveTowardsWaypoint()
    {
        Vector3 deltaVector = waypoints[index].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        RotateTowardsWaypoint(direction);

        if(moving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        if(deltaVector.magnitude <= minDistance)
        {
            StartCoroutine(StayInPlace());
            StartCoroutine(WaitToRotate());
            if (index >= waypoints.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    private void RotateTowardsWaypoint(Vector3 direction)
    {
        if(rotating){
            transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator StayInPlace(){
        moving = false;
        yield return new WaitForSeconds(stayTime);
        moving = true;
    }

    IEnumerator WaitToRotate(){
        yield return new WaitForSeconds(holdRotationTime);
        rotating = true;
        yield return new WaitForSeconds(stayTime - holdRotationTime);
        rotating = false;
    }
}
