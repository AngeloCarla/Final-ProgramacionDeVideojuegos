using System.Windows.Input;
using UnityEngine;
using UnityEngine.Rendering;

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
    private IMovementStrategy strategy;
    private ICommand command;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        strategy = new WalkMovement(walkSpeed);
    }

    void Update()
    {
        // --- Movimiento ---

        // Controla movimiento si el jugador esta en el suelo
        if (cc.isGrounded)
        {
            // Determina la velocidad actual (Shift para correr, sino caminar)
            strategy = Input.GetKey(KeyCode.LeftShift) // condición ? si-es-verdadero : si-es-falso
                ? new RunMovement(runSpeed) : new WalkMovement(walkSpeed);

            // Aplica la velocidad al movimiento
            Vector3 move = strategy.GetMovement(transform);

            moveDirection = move;
            // Salto (tecla Espacio)
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpForce;
        }

        // Aplicar gravedad constantemente
        moveDirection.y -= gravity * Time.deltaTime;

        // Mover al jugador
        command = new MoveCommand(cc, moveDirection);
        command.Execute();
    }
}
