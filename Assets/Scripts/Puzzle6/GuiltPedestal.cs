using UnityEngine;

public class GuiltPedestal : MonoBehaviour
{
    [SerializeField] private bool itemPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GuiltItem>())
        {
            itemPlaced = true;
            Debug.Log("Perdon");
        }
    }

    public bool ItemPlaced => itemPlaced;
}
