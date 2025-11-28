using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [Header("Id de la Llave")]
    [SerializeField] private int keyId; // Id de la llave (coincide con posicion del array en DoorSystem)

    public void Interact()
    {
        Debug.Log($"Llave {keyId} recogida!");
        Destroy(gameObject); // Destruye el objeto
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // DoorSystem.Instance.CollectedKey(keyId); // Recolecta la llave
            Debug.Log($"Llave {keyId} recogida!");
            Destroy(gameObject); // Destruye el objeto
        }
    }
    */


}
