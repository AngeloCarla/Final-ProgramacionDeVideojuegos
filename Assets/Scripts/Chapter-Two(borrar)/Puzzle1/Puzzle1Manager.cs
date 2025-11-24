using UnityEngine;

public class Puzzle1Manager : MonoBehaviour
{
    [Header("Antorchas")]
    [SerializeField] private Torch[] torches; //Antorchas

    private bool IsCompleted = false;

    public void CheckPuzzle()
    {
        if (IsCompleted) return;

        // Todas las antorchas sanas apagadas
        foreach (var t in torches)
        {
            if (!t.IsBroken() && t.IsOn()) // Todas deben estar apagadas
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
