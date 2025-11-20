using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    // Script base para cualquier objeto que el jugador pueda recoger o eliminar del escenario 
    [Header("Configuracion")]
    public string objectName = "Objeto";
    [SerializeField] private bool isWood = false;
    [SerializeField] private bool isExtra = false;

    private CollectWoodMission mission;
    private AmbitionPuzzleManager puzzle1;
    void Start()
    {
        mission = FindAnyObjectByType<CollectWoodMission>();
        puzzle1 = FindAnyObjectByType<AmbitionPuzzleManager>();
        objectName = gameObject.name;
    }

    public void PickUp()
    {
        // Ejemplo: "Recoge" el objeto
        Debug.Log($"Recogiste {objectName}");

        if (puzzle1 != null)
        {
            puzzle1.AddFragment(isExtra);
        }

        /*
        if (isWood && mission != null)
        {
            mission.AddWood();
        }
        */
        Destroy(gameObject);
    }
}
