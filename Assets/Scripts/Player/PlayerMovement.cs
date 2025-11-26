<<<<<<< HEAD
﻿using System.Windows.Input;
=======
>>>>>>> 48048ab118bb16bfffee530b1da36803a1c2da18
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 12f;
    public float runSpeed = 21f;
    private float currentSpeed;

    [Header("Salto")]
    public float jumpForce = 21f;
    public LayerMask groundMask;

    [Header("Componentes")]
    private Rigidbody rb;

<<<<<<< HEAD
    [Header("Audio de Movimiento")]
    public AudioSource audioSource;
    public AudioClip walkStep;   // sonido al caminar
    public AudioClip runStep;    // sonido al correr

    private Vector3 moveDirection = Vector3.zero;
    private IMovementStrategy strategy;
    private ICommand command;
=======
    [Header("Estado del tutorial")]
    public bool movementLocked = true; // El tutorial controla esto
>>>>>>> 48048ab118bb16bfffee530b1da36803a1c2da18

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true; // evita rotaciones raras
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (movementLocked)
        {
<<<<<<< HEAD
            // Determina la velocidad actual (Shift para correr, sino caminar)
            strategy = Input.GetKey(KeyCode.LeftShift) // condición ? si-es-verdadero : si-es-falso
                ? new RunMovement(runSpeed) : new WalkMovement(walkSpeed);

            // Aplica la velocidad al movimiento
            Vector3 move = strategy.GetMovement(transform);

            moveDirection = move;
            // Salto (tecla Espacio)
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpForce;
=======
            // Bloquea el movimiento sin afectar gravedad
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
>>>>>>> 48048ab118bb16bfffee530b1da36803a1c2da18
        }

        HandleMovementInput();
        HandleJump();
    }

<<<<<<< HEAD
        // Mover al jugador
        command = new MoveCommand(cc, moveDirection);
        command.Execute();

        HandleFootsteps();
    }
    void HandleFootsteps()
    {
        // 1. Condición de No Movimiento

        // Primero, verifica si el jugador no está en el suelo o no tiene input de movimiento.
        // Usaremos el movimiento horizontal (X y Z) en lugar de las teclas, que es más limpio.

        // El 'moveDirection' ya contiene la dirección de movimiento horizontal del jugador
        // si el jugador presionó W, A, S o D.
        bool isMoving = (moveDirection.x != 0 || moveDirection.z != 0) && cc.isGrounded;

        if (!isMoving)
        {
            // Si no se está moviendo, y el audio está sonando, detenlo inmediatamente.
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return; // Sal de la función Update
        }

        // 2. Control de Reproducción (Si el jugador se está moviendo)

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        AudioClip targetClip = isRunning ? runStep : walkStep;

        // Si el AudioSource no está sonando AHORA
        if (!audioSource.isPlaying)
        {
            // Asigna el audio de destino y comienza a reproducirlo.
            audioSource.clip = targetClip;
            audioSource.loop = true; // Importante: Aségurate de que haga loop
            audioSource.Play();
        }
        // Si el AudioSource SÍ está sonando, pero necesita cambiar de audio (de caminar a correr o viceversa)
        else if (audioSource.clip != targetClip)
        {
            // Detiene la reproducción actual, asigna el nuevo clip y comienza a sonar.
            audioSource.Stop();
            audioSource.clip = targetClip;
            audioSource.Play();
        }
=======
    private void HandleMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal"); // A-D
        float z = Input.GetAxisRaw("Vertical");   // W-S

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        // Correr con SHIFT
        currentSpeed = (Input.GetKey(KeyCode.LeftShift) && z > 0)
            ? runSpeed
            : walkSpeed;

        Vector3 vel = new Vector3(move.x * currentSpeed, rb.linearVelocity.y, move.z * currentSpeed);
        rb.linearVelocity = vel;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
>>>>>>> 48048ab118bb16bfffee530b1da36803a1c2da18
    }
}
