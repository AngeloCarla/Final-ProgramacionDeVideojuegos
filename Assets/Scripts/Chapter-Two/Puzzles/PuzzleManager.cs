using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RotatingPiece[] piezas;

    private bool isCorrect;

    public void CheckPuzzle()
    {
        if (isCorrect) return;

        foreach (var pieza in piezas)
        {
            if (!pieza.IsCorrect())
            {
                return;
            }
        }

        PuzzleCompletado();
    }

    public void PuzzleCompletado()
    {
        isCorrect = true;
        Debug.Log("Felicidades, ganaste");
    }
}
