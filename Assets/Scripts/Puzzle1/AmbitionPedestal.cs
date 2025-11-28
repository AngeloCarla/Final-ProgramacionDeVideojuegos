using UnityEngine;

public class AmbitionPedestal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private AmbitionPuzzleManager manager; // Manager

    [Header("Entrega y llave")]
    [SerializeField] private GameObject key; // Prefab KeyAmbition
    [SerializeField] private Transform pedestal; // PointKey

    private bool delivered = false;

    public void Update()
    {
        if (delivered) return;

        if (manager != null && manager.IsCompleted)
        {
            Invoke("DeliverKey", 1f);
        }
    }

    public void DeliverKey()
    {
        delivered = true;
        Instantiate(key, pedestal.position, pedestal.rotation); // Instancia la llave
    }
}
