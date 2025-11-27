using UnityEngine;

public class FlashArea : MonoBehaviour
{
   [SerializeField] private Flashlight flashlight; // Linterna

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¿Que es eso?");
            flashlight.StartFlash(); // Inicia el parpadeo
            Destroy(gameObject); // Destruye para evitar repedicion
        }
    }
}
