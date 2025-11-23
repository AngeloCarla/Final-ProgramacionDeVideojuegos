using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWoodMission : MonoBehaviour
{
    // --- Referencias de la IA ---
    [Header("Wendigo AI")]
    // ¡IMPORTANTE! Debes arrastrar el objeto Wendigo a esta ranura en el Inspector.
    [SerializeField] private WendigoAI wendigoAI;
    private WendigoAudio wendigoAudio; // Necesario para los gritos globales

    [Header("Player")]
    [SerializeField] private Transform Player;
    public Transform PlayerTransform => Player;

    [Header("Progreso")]
    [SerializeField] private int requiredWood = 10; // Total Madera
    private int currentWood = 0; // Cantidad actual de Madera

    [Header("Puente")]
    [SerializeField] private GameObject completedBridge; // Puente arreglado
    [SerializeField] private GameObject brokenBridge; // Puente roto

    [Header("Timer")]
    [SerializeField] private float acechoTime = 60f; // Duración total del acecho (60 segundos)
    private Coroutine countdownRoutine;

    private bool collectionStarted = false;
    private bool gameOver = false;

    void Start()
    {
        if (completedBridge) completedBridge.SetActive(false);
        if (brokenBridge) brokenBridge.SetActive(false);

        if (wendigoAI != null)
        {
            wendigoAudio = wendigoAI.GetComponent<WendigoAudio>();
            wendigoAI.SetState(AIState.Dormant);
        }
        else
        {
            Debug.LogError("Referencia a WendigoAI faltante en CollectWoodMission. ¡Arrastra el Wendigo!");
        }
    }

    public void EnableBrokenBridge()
    {
        if (brokenBridge) brokenBridge.SetActive(true);
    }

    public void StartCollectionPhase()
    {
        if (collectionStarted || gameOver) return;

        collectionStarted = true;

        // --- FASE 1: Primer Grito y Comienza el Contador ---
        if (wendigoAudio != null)
        {
            // 1. Asegúrate de que el objeto está ACTIVO antes de reproducir el audio.
            if (wendigoAI != null)
            {
                wendigoAI.gameObject.SetActive(true);
            }

            wendigoAudio.PlayScream();
        }
        Debug.Log("FASE 1: Primer Grito. ¡El tiempo de Acecho ha comenzado!");

        // Inicia el contador de acecho
        countdownRoutine = StartCoroutine(StartAcechoCountdown());
    }

    public void AddWood()
    {
        if (!collectionStarted) return;

        currentWood++;
        Debug.Log($"Madera Recolectada: {currentWood}/{requiredWood}");
    }

    public void DeliverWood()
    {
        if (currentWood >= requiredWood && collectionStarted)
        {
            CompleteMission();
        }
        else if (collectionStarted)
        {
            Debug.Log("Aún no tienes suficiente madera para reparar el puente.");
        }
    }

    public bool HasEnoughWood()
    {
        return currentWood >= requiredWood;
    }

    private IEnumerator StartAcechoCountdown()
    {
        float currentTime = acechoTime;

        // Esperamos 1 segundo después del primer grito para simular el inicio del "acecho"
        yield return new WaitForSeconds(1f);

        // --- FASE 2: El Acecho Comienza (Wendigo aparece en Idle) ---
        if (wendigoAI != null && wendigoAI.currentState == AIState.Dormant)
        {
            StartAcechoPhaseLogic();
        }

        // Contador principal
        while (currentTime > 0)
        {
            // Opcional: Actualizar UI aquí
            // Debug.Log($"⏱ Tiempo restante: {currentTime:F2} s");
            currentTime -= Time.deltaTime;
            yield return null;
        }

        // El tiempo de Acecho terminó
        FinishCountdown();
    }

    private void StartAcechoPhaseLogic()
    {
        Debug.Log("FASE 2: El Wendigo aparece (Acecho: Idle -> Scream -> Walk).");
        wendigoAI.SetState(AIState.Idle);
    }

    public void CompleteMission()
    {
        if (gameOver) return;

        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        // Detiene al Wendigo (Victoria del jugador)
        if (wendigoAI != null)
        {
            wendigoAI.SetState(AIState.Dormant);
        }

        if (completedBridge) completedBridge.SetActive(true);
        if (brokenBridge) brokenBridge.SetActive(false);
        collectionStarted = false;

        Debug.Log("¡Misión Cumplida! Has reconstruido el puente.");
    }

    public void FinishCountdown()
    {
        if (gameOver) return;

        // --- FASE 3: Persecución Final ---
        Debug.Log("FASE 3: ¡Se acabó el tiempo! El Wendigo inicia la persecución.");
        collectionStarted = false;

        if (wendigoAI != null)
        {
            wendigoAI.SetState(AIState.Run);
        }
    }

    /// <summary>
    /// Llamado por WendigoAI al detectar colisión con el jugador (Game Over).
    /// </summary>
    public void YouLose()
    {
        if (gameOver) return;
        gameOver = true;
        collectionStarted = false;

        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        Debug.Log("YOU LOSE: El Wendigo te ha atrapado.");
        // Pausa el juego y muestra la pantalla de derrota
        Time.timeScale = 0f;
        // Implementar la lógica para mostrar tu UI de "You Lose" aquí.
    }
}