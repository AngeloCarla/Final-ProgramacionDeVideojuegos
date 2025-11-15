using System.Security.Cryptography;
using UnityEngine;

public class RotatingPiece : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private Puzzle2Manager manager; // Manager

    [Header("Rotacion")]
    [SerializeField] private float rotationStep = 90f; // Cuanto gira la pieza
    [SerializeField] private float correct = 0f; // Angulo correcto
    [SerializeField] private float tolerance = 5f; // Tolerancia para permitir variaciones

    public void Start()
    {
        manager = FindAnyObjectByType<Puzzle2Manager>();
    }

    public void Interact()
    {
        transform.Rotate(0, rotationStep, 0); // Rota la pieza en Y según el paso definido

        manager?.CheckPuzzle();
    }

    public bool IsCorrect()
    {
        // Obtiene la rotación actual en Y
        float y = transform.eulerAngles.y;

        // Devuelve true si la rotación está dentro del margen de tolerancia
        return Mathf.Abs(y - correct) <= tolerance;
    }
}
