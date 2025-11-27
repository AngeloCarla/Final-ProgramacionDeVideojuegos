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

    [Header("Audio de Movimiento")]
    public AudioSource audioSource;
    public AudioClip walkStep;   // sonido al caminar
    public AudioClip runStep;    // sonido al correr

    private Vector3 moveDirection = Vector3.zero;
    private IMovementStrategy strategy;
    private ICommand command;
    public bool movementLocked = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        strategy = new WalkMovement(walkSpeed);
    }

    void Update()
    {
        // --- Movimiento ---

        // Aplicar gravedad constantemente
        moveDirection.y -= gravity * Time.deltaTime;

        if (movementLocked)
        {
            // Si el movimiento está bloqueado, aún debe aplicarse la gravedad
            command = new MoveCommand(cc, moveDirection);
            command.Execute();
            return;
        }

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

            // Reiniciar Y cuando está en el suelo (para evitar acumulación de gravedad)
            if (moveDirection.y < 0)
            {
                moveDirection.y = -0.5f; // Fuerza mínima para mantener el contacto
            }
        }

        // Mover al jugador
        command = new MoveCommand(cc, moveDirection);
        command.Execute();

        HandleFootsteps();
    }
    void HandleFootsteps()
    {

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

        // 2. Control de Reproducción

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        AudioClip targetClip = isRunning ? runStep : walkStep;

        // Si el AudioSource no está sonando AHORA
        if (!audioSource.isPlaying)
        {
            audioSource.clip = targetClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        // Si el AudioSource SÍ está sonando, pero necesita cambiar de audio
        else if (audioSource.clip != targetClip)
        {
            // Detiene la reproducción actual, asigna el nuevo clip y comienza a sonar.
            audioSource.Stop();
            audioSource.clip = targetClip;
            audioSource.Play();
        }
    }
}