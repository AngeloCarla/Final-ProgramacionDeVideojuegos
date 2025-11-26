using UnityEngine;
using UnityEngine.Rendering;

public class GuiltItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private bool isHeld = false;

    private Rigidbody rb;
    private Collider collider;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (!isHeld)
        {
            PickUp();
        }
        else
        {
            Drop();
        }
    }

    public void PickUp()
    {
        isHeld = true;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
        collider.enabled = false;
    }

    public void Drop()
    {
        isHeld = false;

        transform.SetParent(null);
        rb.isKinematic = false;
        collider.enabled = true;
    }
}
