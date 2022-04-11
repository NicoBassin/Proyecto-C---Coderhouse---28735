using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("A");
        if(hit.gameObject.CompareTag("Door")){
            OpenDoor(hit.gameObject);
        }
    }

    private void OpenDoor(GameObject door){
        Animator doorAnimator = door.GetComponent<Animator>();
        bool currentState = doorAnimator.GetBool("isOpened");
        Debug.Log("B");
        doorAnimator.SetBool("isOpened", !currentState);
    }
}
