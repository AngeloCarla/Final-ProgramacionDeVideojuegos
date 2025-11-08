using System.Windows.Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;

    [Header("Movimiento del Personaje")]
    [SerializeField] private float walkSpeed = 6.0f; // Velocidad al caminar
    [SerializeField] private float runSpeed = 10.0f; // Velocidad al correr

    [Header("Salto del Personaje (prueba)")]
    [SerializeField] private float jumpForce = 8.0f; // Impulso de salto
    [SerializeField] private float gravity = 20.0f; // Gravedad

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- Movimiento ---

        // Controla movimiento si el jugador esta en el suelo
        if (cc.isGrounded)
        {
            
            // Leer ejes de entrada (A/D y W/S)
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Calcula la direccion del movimiento en base a la orientacion del jugador
            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            // Determina la velocidad actual (Shift para correr, sino caminar)
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            // Aplica la velocidad al movimiento
            moveDirection = move * currentSpeed;

            // Salto (tecla Espacio)
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpForce;
        }

        // Aplicar gravedad constantemente
        moveDirection.y -= gravity * Time.deltaTime;

        // Mover al jugador usando CharacterController
        cc.Move(moveDirection * Time.deltaTime);
    }
}
