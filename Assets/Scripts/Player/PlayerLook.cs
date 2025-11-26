using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Cámara del jugador")]
    [SerializeField] private Camera playerCamera;

    [Header("Opciones")]
    [SerializeField][Range(0, 100)] private float sensitivity;

    private float vRotation = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Previene rotaciones físicas no deseadas
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Rotación vertical (solo cámara)
        vRotation -= mouseY;
        vRotation = Mathf.Clamp(vRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);

        // Rotación horizontal (CUERPO usando Rigidbody)
        Quaternion deltaRotation = Quaternion.Euler(0, mouseX, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
