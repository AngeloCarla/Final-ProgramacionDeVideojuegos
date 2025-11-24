using UnityEngine;
using System.Collections;

public class ControlPuzzleManager : MonoBehaviour
{
    [Header("Estatuas y puerta")]
    [SerializeField] private Statue[] statues; // Estatuas
    [SerializeField] private GameObject door; // Puerta donde esta la llave

    private float idleTimeNeeded = 15f; // Tiempo de inactividaad necesario
    private float idleTimer = 0; // Contador de inactividad

    private bool completed = false;

    public void Update()
    {
        if (completed) return;

        // Cuenta el tiempo sin tocar nada
        idleTimer += Time.deltaTime;

        // Cuando el jugador abandona el control y no toca lo suficiente
        if (idleTimer >= idleTimeNeeded)
        {
            SolvePuzzle(); // Completa el desafio
        }

       // Debug.Log($"TIEMPO: {idleTimer:F2} s"); // prueba
    }

    public void OnPlayerInteraction()
    {
        if (completed) return;

        // Cada vez que el jugador interactua con la estatua se reinicia el contador
        idleTimer = 0;
    }

    public void SolvePuzzle()
    {
        completed = true;

        foreach (var s in statues)
        {
            s.AutoAlign();
        }

        Invoke("OpenDoor", 2f);
    }

    public void OpenDoor()
    {
        door.SetActive(false); // Se abre la puerta (Para recoger la llave)
        Debug.Log("Felicidades");
    }
}
