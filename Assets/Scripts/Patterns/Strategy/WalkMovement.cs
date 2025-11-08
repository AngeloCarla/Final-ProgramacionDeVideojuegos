using UnityEngine;

public class WalkMovement : IMovementStrategy
{
    private float walkSpeed; // Velocidad

    public WalkMovement(float walkSpeed)
    {
        this.walkSpeed = walkSpeed;
    }

    public Vector3 GetMovement(Transform transform)
    {
        // Leer ejes de entrada (A/D y W/S)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula la direccion del movimiento en base a la orientacion del jugador
        return (transform.right * moveX + transform.forward * moveZ) * walkSpeed;
    }
}
