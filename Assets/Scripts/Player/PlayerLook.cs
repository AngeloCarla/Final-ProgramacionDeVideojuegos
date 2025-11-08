using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camara del jugador")]
    [SerializeField] private Camera playerCamera;

    [Header("Opciones")]
    [SerializeField] private float sensitivity; // Sensibilidad del mouse
    private float vRotation = 0f; // Rotacion Vertical
    void Update()
    {
        // --- Rotacion ---
        // Eje X: rota el cuerpo del jugador horizontalmente (izquierda/derecha con el mouse)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(0, mouseX, 0);

        // Eje Y: rota la cámara verticalmente (mirar arriba/abajo con el mouse)
        vRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        vRotation = Mathf.Clamp(vRotation, -60f, 60f); // Limite la rotacion vertical
        playerCamera.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);
    }
}
