using System.Collections.Generic;
using UnityEngine;

public class CollectWoodMission : MonoBehaviour
{
    [Header("Progreso")]
    [SerializeField] private int requiredWood = 10; // Total Madera
    private int currentWood = 0; // Cantidad actual de Madera

    [Header("Caminos")]
    [SerializeField] private List<GameObject> completedBridge; // Puente arreglado
    [SerializeField] private List<GameObject> brokenBridge; // Puente roto
    [SerializeField] private GameObject mysteriousPath; // Camino misterioso

    private bool collectionStarted = false;

    void Start()
    {
        foreach (var part in brokenBridge)
            part.SetActive(true);

        foreach (var part in completedBridge)
            part.SetActive(false);

        if (mysteriousPath) mysteriousPath.SetActive(false);
    }

    public void EnableBrokenBridge()
    {
        foreach (var part in brokenBridge)
            part.SetActive(true);
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

        if (currentWood <= completedBridge.Count)
        {
            completedBridge[currentWood - 1].SetActive(true);
            brokenBridge[currentWood - 1].SetActive(false);
        }

        if (currentWood == requiredWood - 1)
        {
            Invoke(nameof(EnableMysteriousPath), 2f);
        }
    }

    public void EnableMysteriousPath()
    {
        if (mysteriousPath)
        {
            Debug.Log("Un nuevo camino se abre entre los árboles... (camino alternativo)");
            mysteriousPath.SetActive(true);
        }
    }

    public bool HasEnoughWood()
    {
        return currentWood >= requiredWood;
    }

    public void CompleteMission()
    {
        foreach (var part in brokenBridge)
            part.SetActive(true);

        foreach (var part in completedBridge)
            part.SetActive(true);

        Debug.Log("Has reconstruido el puente. ¡Puedes cruzar!");
    }
}
