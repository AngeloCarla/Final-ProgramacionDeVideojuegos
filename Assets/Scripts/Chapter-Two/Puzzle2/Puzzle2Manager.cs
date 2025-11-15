using UnityEngine;

public class Puzzle2Manager : MonoBehaviour
{
    [Header("Piezas")]
    [SerializeField] private RotatingPiece[] piezas; // Piezas que rotan

    private bool IsCompleted = false;

    public void CheckPuzzle()
    {
        if (IsCompleted) return;

        // Todas las piezas estan rotadas correctamente
        foreach (var pieza in piezas)
        {
            if (!pieza.IsCorrect()) // Piesas alineadas
            {
                return;
            }
        }

        PuzzleCompleted();
    }

    public void PuzzleCompleted()
    {
        // Puzzle completo
        IsCompleted = true;
        Debug.Log("Felicidades, ganaste");
    }
}
