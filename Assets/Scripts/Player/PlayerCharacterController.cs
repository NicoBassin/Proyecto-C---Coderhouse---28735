using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacterController : MonoBehaviour
{
    public event Action OnJump;

    // CharacterController (Componente).
    private CharacterController ccPlayer;

    // Variables para el salto.
    private Vector3 playerJump;
    private float jumpHeight = 1.5f;
    private float gravity = -9.81f;
    private bool canJump = true;
    private float originalStepOffset;

    // Transform del PlayerLook para mover la cámara.
    [SerializeField] private Transform playerLookAt;
    private float xAxisRotation = 0f;
    private float yAxisRotation = 0f;
    private float xMinRotation = 0f;
    private float xMaxRotation = 25f;
    [SerializeField] private float sensibility = 2.5f;

    // Variables para el movimiento.
    [SerializeField] private float playerSpeed = 3.5f;
    private bool isRunning = false;
    private bool canMove = true;
    private float timeOne = 1.84f;
    private float timeTwo = 1.51f;
    private float jumpDelay = 0.5f;
    private bool isJumping = false;
    private float jumpAnimationDelay = 1.9f;

    // Start is called before the first frame update
    void Start()
    {
        ccPlayer = GetComponent<CharacterController>();
        originalStepOffset = ccPlayer.stepOffset;
        FindObjectOfType<PlayerAttack>().OnAttackOne += AttackingOne;
        FindObjectOfType<PlayerAttack>().OnAttackTwo += AttackingTwo;
        FindObjectOfType<PlayerCharacterController>().OnJump += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRotate();
        PlayerJump();
    }

    private void PlayerJump(){
        // Si salta (en el piso), su velocidad en Y aumenta a razón de √2gh, no puede saltar de nuevo y
        // cambia el StepOffset a 0 para evitar glitches durante el salto.
        if((Input.GetKeyDown(KeyCode.Space)) && (canJump)){
            OnJump?.Invoke();
        }

        // Su velocidad en y disminuye según el valor de la gravedad constantemente (frame independent).
        playerJump.y += gravity * Time.deltaTime;

        // Se mueve según su velocidad en Y (frame independent).
        ccPlayer.Move(playerJump * Time.deltaTime);

        // Si tocó el piso (en el último Move) y no está saltando, su velocidad en Y es 0 y puede saltar de nuevo. Reinicia el StepOffset.
        if(ccPlayer.isGrounded && !isJumping){
            ccPlayer.stepOffset = originalStepOffset;
            playerJump.y = 0f;
            canJump = true;
        }
    }

    private void Jump(){
        canJump = false;
        isJumping = true;
        StartCoroutine(JumpDelay());
    }

    IEnumerator JumpDelay(){
        yield return new WaitForSeconds(jumpDelay);
        ccPlayer.stepOffset = 0f;
        playerJump.y = Mathf.Sqrt(-2.0f * jumpHeight * gravity);
        yield return new WaitForSeconds(jumpAnimationDelay);
        isJumping = false;
    }

    private void PlayerMove(){
        // Si el jugador pulsa WASD se mueve.
        if(Input.GetKey(KeyCode.W)){
            CCMove(Vector3.forward);
        }
        if(Input.GetKey(KeyCode.S)){
            CCMove(Vector3.back);
        }
        if(Input.GetKey(KeyCode.A)){
            CCMove(Vector3.left);
        }
        if(Input.GetKey(KeyCode.D)){
            CCMove(Vector3.right);
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            isRunning = true;
        }
        else{
            isRunning = false;
        }
    }

    private void CCMove(Vector3 direction){
        // Movimiento con Character Controller.
        if(canMove){
            if(isRunning){
                ccPlayer.Move(2.0f * playerSpeed * transform.TransformDirection(direction) * Time.deltaTime);
            }
            else{
                ccPlayer.Move(playerSpeed * transform.TransformDirection(direction) * Time.deltaTime);
            }
        }
    }

    private void PlayerRotate(){
        // Los Inputs x / y (hasta cierto punto) del mouse varían el ángulo de la cámara,
        // Solo el input x del mouse varía la rotación local del jugador.
        xAxisRotation -= Input.GetAxis("Mouse Y") * sensibility;
        yAxisRotation += Input.GetAxis("Mouse X") * sensibility;
        if(xAxisRotation <= xMinRotation){
            xAxisRotation = xMinRotation;
        }
        if(xAxisRotation >= xMaxRotation){
            xAxisRotation = xMaxRotation;
        }
        Quaternion cameraAngle = Quaternion.Euler(xAxisRotation, 0f, 0f);
        Quaternion playerAngle = Quaternion.Euler(0f, yAxisRotation, 0f);
        playerLookAt.localRotation = cameraAngle;
        transform.rotation = playerAngle;
    }

    private void AttackingOne(){
        StartCoroutine(AttackOne());
    }

    private void AttackingTwo(){
        StartCoroutine(AttackTwo());
    }

    IEnumerator AttackOne(){
        canMove = false;
        yield return new WaitForSeconds(timeOne);
        canMove = true;
    }
    
    IEnumerator AttackTwo(){
        canMove = false;
        yield return new WaitForSeconds(timeTwo);
        canMove = true;
    }
}
