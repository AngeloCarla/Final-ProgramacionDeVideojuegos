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

    [Header("Audio de Movimiento")]
    public AudioSource audioSource;
    public AudioClip walkStep;   // sonido al caminar
    public AudioClip runStep;    // sonido al correr

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
    }
}
