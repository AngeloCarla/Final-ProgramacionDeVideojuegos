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
            delivered = true;
            DeliverKey();
        }
    }

    public void DeliverKey()
    {
        Instantiate(key, pedestal.position, pedestal.rotation); // Instancia la llave
    }
}
