using UnityEngine;

public class RunMovement : IMovementStrategy
{
    private float runSpeed; // Velocidad

    public RunMovement(float runSpeed)
    {
        this.runSpeed = runSpeed;
    }

    public Vector3 GetMovement(Transform transform)
    {
        // Leer ejes de entrada (A/D y W/S)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula la direccion del movimiento en base a la orientacion del jugador
        return (transform.right * moveX + transform.forward * moveZ) * runSpeed;
    }
}
