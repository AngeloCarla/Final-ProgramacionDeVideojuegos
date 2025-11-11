using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    // Script base para cualquier objeto que el jugador pueda recoger o eliminar del escenario 
    [Header("Configuracion")]
    public string objectName = "Objeto";
    [SerializeField] private bool isWood = false;

    private CollectWoodMission mission;
    void Start()
    {
        mission = FindAnyObjectByType<CollectWoodMission>();
        objectName = gameObject.name;
    }

    public void PickUp()
    {
        // Ejemplo: "Recoge" el objeto
        Debug.Log($"Recogiste {objectName}");

        if (isWood && mission != null)
        {
            mission.AddWood();
        }

        Destroy(gameObject);
    }
}
