using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Candados ligados")]
    [SerializeField] private GameObject[] locks; // Candados

    private bool opened = false;

    void Update()
    {
        // Si estan todos los candados rotos se abre
        if (!opened && AllLocksBroken())
        {
            OpenDoor();
        }
    }

    private bool AllLocksBroken()
    {
        // Recorre para buscar candados activos
        foreach (GameObject candado in locks)
        {
            if (candado.activeSelf) return false; // Si hay alguno activo, en escena, no se abre
        }
        return true; // Si no hay candados activos, se abre
    }

    public void OpenDoor()
    {
        opened = true;
        Debug.Log("Puerta Abierta");
        Destroy(gameObject); // Destruye la puerta
    }
}
