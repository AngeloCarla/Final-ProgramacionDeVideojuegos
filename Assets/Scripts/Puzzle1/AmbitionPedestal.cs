using UnityEngine;

public class AmbitionPedestal : MonoBehaviour, IInteractable
{
    [Header("Manager")]
    [SerializeField] private AmbitionPuzzleManager manager;

    [Header("Entrega y llave")]
    [SerializeField] private GameObject key;
    [SerializeField] private Transform pedestal;

    private bool delivered = false;

    public void Interact()
    {
        if (delivered) return;

        if (manager != null && manager.IsCompleted)
        {
            DeliverKey();
        }
        else
        {
            Debug.Log("Aun no tienes suficiente");
        }
    }

    public void DeliverKey()
    {
        delivered = true;
        Instantiate(key, pedestal.position, pedestal.rotation);
    }
}
