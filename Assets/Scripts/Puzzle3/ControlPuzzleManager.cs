using UnityEngine;
using System.Collections;

public class ControlPuzzleManager : MonoBehaviour
{
    [Header("Estatuas")]
    [SerializeField] private Statue[] statues; // Estatuas

    private float hintCooldown = 0f; // Conatdor para el mensaje
    private float timeSinceLastHint = 5f;

    private float idleTimeNeeded = 10f; // Tiempo de inactividaad necesario
    private float idleTimer = 0; // Contador de inactividad

    private bool completed = false;

    public void Update()
    {
        if (completed) return;

        // Cuenta el tiempo sin tocar nada
        idleTimer += Time.deltaTime;
        timeSinceLastHint += Time.deltaTime;

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

        // Si pasaron 10 segundos desde la última pista, damos la pista
        if (timeSinceLastHint >= hintCooldown)
        {
            ShowHint();
            timeSinceLastHint = 0f;
        }
    }

    public void ShowHint()
    {
         Debug.Log("No siempre debes controlar todo...");
    }

    public void SolvePuzzle()
    {
        completed = true;

        foreach (var s in statues)
        {
            s.AutoAlign(); // Se alinean las estatuas
        }

        Debug.Log("Superaste el desafio y obtuviste una llave, Felicidades!");
    }

    public bool IsCompleted => completed;
}
