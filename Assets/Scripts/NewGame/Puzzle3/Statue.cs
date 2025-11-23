using UnityEngine;

public class Statue : MonoBehaviour
{
    private float rotationStep = 90f; // Cuanto gira la pieza
    private float correct = 0f; // Angulo correcto
    private float tolerance = 5f; // Tolerancia para permitir variaciones

    [Header("Pieza rota")]
    [SerializeField] private bool isImpossible = false;
    public void RotateStatue()
    {
        transform.Rotate(0, rotationStep, 0); // Rota la pieza en Y según el paso definido
    }

    public bool IsAligned()
    {
        // Obtiene la rotación actual en Y
        float y = transform.eulerAngles.y;

        // Devuelve true si la rotación está dentro del margen de tolerancia
        return Mathf.Abs(y - correct) <= tolerance;
    }

    public bool IsImpossible() => isImpossible;
}
