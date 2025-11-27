using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Cámara del jugador")]
    [SerializeField] private Camera playerCamera;

    [Header("Opciones")]
    [SerializeField][Range(0, 100)] private float sensitivity;

    private float vRotation = 0f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        // Rotación vertical (solo cámara)
        vRotation -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        vRotation = Mathf.Clamp(vRotation, -60f, 60f);
        playerCamera.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);
    }
}
