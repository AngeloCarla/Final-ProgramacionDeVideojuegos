using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lock : MonoBehaviour, IInteractable
{
    [Header("Candado")]
    [SerializeField] private int id; // Id que coincide con su llave

    public void Interact()
    {
        // Si la llave con ese ID ya fue recogida, "rompe" el candado
        if (DoorSystem.Instance.Keys[id])
        {
            BreakLock();
        }
        else
        {
            Debug.Log("No tienes la llave correcta");
        }
    }

    private void BreakLock()
    {
        Debug.Log($"Candado {id} roto");
        gameObject.SetActive(false); // Desactiva el candado
    }
}
