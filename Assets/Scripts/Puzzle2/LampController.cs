using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampController : MonoBehaviour
{
    private Light lightComponent;
    private bool isOn = true;

    private bool playerNearby = false;

    private void Awake()
    {
        lightComponent = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        // SOLO se apaga si el jugador está cerca de ESTA lámpara
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLamp();
        }
    }

    public void ToggleLamp()
    {
        isOn = !isOn;

        if (lightComponent != null)
        {
            lightComponent.intensity = isOn ? 1000f : 0f;
        }
    }

    public bool IsOn()
    {
        return isOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
