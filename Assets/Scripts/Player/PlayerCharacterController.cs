using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    // CharacterController (Componente).
    private CharacterController ccPlayer;

    // Variables para el salto.
    private Vector3 playerJump;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 2.0f;
    private float gravity = -9.81f;
    private bool canJump = true;
    private float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        ccPlayer = GetComponent<CharacterController>();
        originalStepOffset = ccPlayer.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    private void PlayerJump(){
        // Si salta (en el piso), su velocidad en Y aumenta a razón de √2gh, no puede saltar de nuevo y
        // cambia el StepOffset a 0 para evitar glitches durante el salto.
        if((Input.GetKeyDown(KeyCode.Space)) && (canJump)){
            ccPlayer.stepOffset = 0f;
            playerJump.y = Mathf.Sqrt(-2.0f * jumpHeight * gravity);
            canJump = false;
        }

        // Su velocidad en y disminuye según el valor de la gravedad constantemente (frame independent).
        playerJump.y += gravity * Time.deltaTime;

        // Se mueve según su velocidad en Y (frame independent).
        ccPlayer.Move(playerJump * Time.deltaTime);

        // Si tocó el piso (en el último Move), su velocidad en Y es 0 y puede saltar de nuevo. Reinicia el StepOffset.
        if(ccPlayer.isGrounded){
            ccPlayer.stepOffset = originalStepOffset;
            playerJump.y = 0f;
            canJump = true;
        }
    }

    private void PlayerMove(){
        // Si el jugador pulsa WASD se mueve.
        if(Input.GetKey(KeyCode.W)){
            PlayerMove(Vector3.forward);
        }
        if(Input.GetKey(KeyCode.S)){
            PlayerMove(Vector3.back);
        }
        if(Input.GetKey(KeyCode.A)){
            PlayerMove(Vector3.left);
        }
        if(Input.GetKey(KeyCode.D)){
            PlayerMove(Vector3.right);
        }
    }

    private void PlayerMove(Vector3 direction){
        // Movimiento con Character Controller.
        ccPlayer.Move(playerSpeed * transform.TransformDirection(direction) * Time.deltaTime);
    }
}
