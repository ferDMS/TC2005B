using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Variables para el movimiento del personaje
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rig;
    // Girar el sprite para que camine de frente
    public SpriteRenderer sr;

    // Variable para limitar saltos de personaje
    public bool canJump = true;

    // Variables para cambiar entre animaciones según condiciones
    // Donde se coloca el controlador de animaciones directo en Unity
    Animator animatorController; 

    // Start is called before the first frame update
    void Start()
    {
        // Para que desde el inicio empiece con la animación default
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            // Cuando el player está saltando, actualizar la animación a salto
            if(rig.velocity.y != 0)
            {
                UpdateAnimation(PlayerAnimation.jump);
            }
        }
    }

    // Como update pero más rápido
    // Predefinido y llamado por Unity
    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(xInput * moveSpeed, rig.velocity.y);
        if (xInput != 0 && rig.velocity.y == 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        }
        else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }

        // Checar si vamos a la izquierda o derecha para cambiar el sprite en su escala.
        // Al cambiar la escala se puede hacerlo espejo en el eje x según la dirección de movimiento
        if (rig.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rig.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    // Listado de las animaciones disponibles para el personaje
    // Esto 
    public enum PlayerAnimation
    {
        idle, walk, jump
    }

    // Revisar la animación actual y actualizarla a la correcta
    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        // Cambiar los parámetros del estado de movimiento según el que se desea
        switch(nameAnimation)
        {
            // Se desea estar idle, entonces desactivar los dos parámetros
            case PlayerAnimation.idle:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", false);
                break;
            // Activar walk y desactivar jump
            case PlayerAnimation.walk:
                animatorController.SetBool("isWalking", true);
                animatorController.SetBool("isJumping", false);
                break;
            // Activar jump y desactivar walk
            case PlayerAnimation.jump:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", true);
                break;
        }
    }
}
