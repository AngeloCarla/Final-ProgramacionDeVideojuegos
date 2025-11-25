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

    [Header("Estado del tutorial")]
    public bool movementLocked = true; // El tutorial controla esto

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
            // Bloquea el movimiento sin afectar gravedad
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        HandleMovementInput();
        HandleJump();
    }

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
    }
}
