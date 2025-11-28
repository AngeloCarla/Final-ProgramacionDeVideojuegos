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
            Invoke("DeliverKey", 1f);
        }
    }

    public void DeliverKey()
    {
        delivered = true;
        Instantiate(key, pedestal.position, pedestal.rotation); // Instancia la llave
    }
}
