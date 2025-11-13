using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWoodMission : MonoBehaviour
{
    [Header("Progreso")]
    [SerializeField] private int requiredWood = 10; // Total Madera
    private int currentWood = 0; // Cantidad actual de Madera

    [Header("Puente")]
    [SerializeField] private GameObject completedBridge; // Puente arreglado
    [SerializeField] private GameObject brokenBridge; // Puente roto

    [Header("Timer")]
    [SerializeField] private float countdown = 10;
    private float currentTime = 0;
    private bool isRunningCountdown = false;
    private Coroutine countdownRoutine;

    private bool collectionStarted = false;

    void Start()
    {
        if (completedBridge) completedBridge.SetActive(false);
        if (brokenBridge) brokenBridge.SetActive(false);
    }
    /*
    void Update()
    {
        if (isRunningCountdown)
        {
            currentTime -= Time.deltaTime;
            Debug.Log($"⏱ Tiempo Restante: {currentTime:F2} s");

            if (currentTime <= 0)
            {
                FinishCountdown();
            }
        }
    }
    */

    public void EnableBrokenBridge()
    {
        if (brokenBridge) brokenBridge.SetActive(true);
    }

    public void StartCollectionPhase()
    {
        collectionStarted = true;

        //currentTime = countdown;
        //isRunningCountdown = true;
        Debug.Log($"Tienes {countdown} segundos para recolectar toda la madera");
        countdownRoutine = StartCoroutine(StartCountdown());
    }

    public void AddWood()
    {
        if (!collectionStarted) return;

        currentWood++;
        Debug.Log($"Madera Recolectada: {currentWood}/{requiredWood}");
    }


    public void DeliverWood()
    {
        if (currentWood >= requiredWood)
        {
            CompleteMission();
        }
    }

    public bool HasEnoughWood()
    {
        return currentWood >= requiredWood;
    }

    private IEnumerator StartCountdown()
    {
        float currentTime = countdown;

        while (currentTime > 0)
        {
            Debug.Log($"⏱ Tiempo restante: {currentTime:F2} s");
            currentTime -= Time.deltaTime;
            yield return null;
        }

        FinishCountdown();
    }

    public void CompleteMission()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        if (completedBridge) completedBridge.SetActive(true);
        Debug.Log("Has reconstruido el puente. ¡Puedes cruzar!");
    }

    public void FinishCountdown() { 
    
        //isRunningCountdown = false;
        collectionStarted = false;

        Debug.Log("Se acabo el tiempo");
    }
}
