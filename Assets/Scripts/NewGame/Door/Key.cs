using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Id de la Llave")]
    [SerializeField] private int keyId; // Id de la llave (coincide con posicion del array en DoorSystem)

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorSystem.Instance.CollectedKey(keyId); // Recolecta la llave
            Destroy(gameObject); // Destruye el objeto
        }
    }
}
