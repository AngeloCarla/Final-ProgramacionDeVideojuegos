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

    private bool collectionStarted = false;

    void Start()
    {
        if (completedBridge) completedBridge.SetActive(false);
        if (brokenBridge) brokenBridge.SetActive(false);
    }

    public void EnableBrokenBridge()
    {
        if (brokenBridge) brokenBridge.SetActive(true);
    }

    public void StartCollectionPhase()
    {
        collectionStarted = true;
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

    public void CompleteMission()
    {
        if (completedBridge) completedBridge.SetActive(true);
        Debug.Log("Has reconstruido el puente. ¡Puedes cruzar!");
    }
}
