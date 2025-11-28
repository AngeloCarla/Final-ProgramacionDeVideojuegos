using UnityEngine;

public class ControlPedestal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private ControlPuzzleManager manager; // Manager

    [Header("Entrega y llave")]
    [SerializeField] private GameObject key; // Prefab KeyControl
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
