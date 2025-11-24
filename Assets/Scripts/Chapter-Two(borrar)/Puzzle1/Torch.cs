using UnityEngine;

public class Torch : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private Puzzle1Manager manager; // Manager del puzzle

    [Header("Antorcha (Luz)")]
    [SerializeField] private Light torch; // Antorcha (luz)

    [Header("Estados")]
    [SerializeField] private bool isBroken = false; // Para la antorcha rota
    private bool isOn = true; // Estado de la antorcha (Encendido/Apagado)

    public void Start()
    {
        manager = FindAnyObjectByType<Puzzle1Manager>();
        ApplyLightState(); // Mantiene el estado inicial
    }

    public void Interact()
    {
        if (isBroken) return; // Si esta rota, no interactua
        ToggleTorch();
    }

    public void ToggleTorch()
    {
        isOn = !isOn; // Cambia On/Off
        ApplyLightState(); // Actualiza la luz

        manager?.CheckPuzzle();
    }

    public void ApplyLightState()
    {
        if (torch) torch.enabled = isOn; // Actualiza segun el estado
    }

    public void SetOn(bool value)
    {
        isOn = value;
        ApplyLightState();
    }

    public bool IsOn() => isOn;
    public bool IsBroken() => isBroken;
}
