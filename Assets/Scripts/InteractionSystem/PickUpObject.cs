using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    // Script base para cualquier objeto que el jugador pueda recoger o eliminar del escenario
    [Header("Configuracion")]
    public string objectName = "Objeto";
    void Start()
    {
        objectName = gameObject.name; // Usa el nombre del objeto como base

        // Si tiene tag distinto a "Untagged", lo agrega entre parentesis
        if (tag != "Untagged")
        {
            objectName += $" ({tag})";
        }
    }

    public void PickUp()
    {
        // Ejemplo: "Recoge" el objeto
        Debug.Log($"Recogiste {objectName}");
        Destroy(gameObject);
    }
}
