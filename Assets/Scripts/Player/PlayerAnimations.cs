using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    private Animator playerAnimator;
    private Quaternion initRotation;

    private float crossfadeTime = 0.3f;
    private int keysPressed = 0;
    private bool isRunning = false;
    private bool isCasting = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = playerModel.GetComponent<Animator>();
        initRotation = playerModel.transform.localRotation;
        FindObjectOfType<PlayerCharacterController>().OnJump += Jump;
        FindObjectOfType<PlayerAttack>().OnAttackOne += AttackOne;
        FindObjectOfType<PlayerAttack>().OnAttackTwo += AttackTwo;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void AttackOne(){
        playerAnimator.CrossFade("AttackOne", crossfadeTime);
    }

    private void AttackTwo(){
        playerAnimator.CrossFade("AttackTwo", crossfadeTime);
    }

    private void Death(){
        playerAnimator.CrossFade("Death", crossfadeTime);
    }

    private void Jump(){
        playerAnimator.CrossFade("Jump", crossfadeTime);
    }

    IEnumerator Casting(float castingTime){
        isCasting = true;
        yield return new WaitForSeconds(castingTime);
        isCasting = false;
    }

    private void Moving(){
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            if(keysPressed == 0 && !isCasting){
                playerAnimator.CrossFade("Walk", crossfadeTime);
            }
            keysPressed++;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)){
            if(keysPressed == 1 && !isCasting){
                playerAnimator.CrossFade("Idle", crossfadeTime);
            }
            keysPressed--;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(keysPressed > 0 && !isCasting){
                playerAnimator.CrossFade("Run", crossfadeTime);
            }
            isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            if(keysPressed > 0 && !isCasting){
                playerAnimator.CrossFade("Walk", crossfadeTime);
            }
            isRunning = false;
        }

        if(keysPressed == 0){
            playerAnimator.SetBool("isWalking", false);
        }
        if(keysPressed > 0){
            playerAnimator.SetBool("isWalking", true);
            if(isRunning){
                playerAnimator.SetBool("isRunning", true);
            }
            else{
                playerAnimator.SetBool("isRunning", false);
            }
        }
    }

    /*IEnumerator TransformCorrection(){
        yield return new WaitForSeconds(correctionTime);
        Vector3 currentHeight = Vector3.zero;
        currentHeight.y = playerModel.transform.position.y;

        playerModel.transform.position -= currentHeight;
        playerModel.transform.localRotation = initRotation;
    }*/
}
