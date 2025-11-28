using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Linterna")]
    [SerializeField] private GameObject flashlight;

    [Header("Señal")]
    [SerializeField] private float flashInterval = 0.02f; // Cada cuanto parpadea
    [SerializeField] private float flashDuration = 3.5f; // Cuanto dura

    private bool isFlashing = false;

    public void Update()
    {
        // Enciende y apaga la linterna
        if (Input.GetKeyUp(KeyCode.F))
        {
            ToggleFlash();
        }

        // TEST rápido: apretá G para que empiece el parpadeo
        if (Input.GetKeyUp(KeyCode.G))
        {
            StartFlash();
        }
    }

    public void ToggleFlash()
    {
        flashlight.SetActive(!flashlight.activeSelf);
    }

    public void StartFlash()
    {
        if (isFlashing) return;
        isFlashing = true;

        InvokeRepeating("ToggleFlash", 0f, flashInterval); // Parpadeo
        Invoke("StopFlash", flashDuration); // Para el parpadeo
    }

    public void StopFlash()
    {
        CancelInvoke("ToggleFlash");
        isFlashing = false;
        flashlight.SetActive(true);
    }
}
