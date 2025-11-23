using UnityEngine;
using UnityEngine.Rendering;

public class ControlPuzzleManager : MonoBehaviour
{
    [Header("Estatuas y puerta")]
    [SerializeField] private Statue[] statues; // Estatuas
    [SerializeField] private GameObject door; // Puerta donde esta la llave

    private bool completed = false;

    public void Update()
    {
        if (!completed)
        {
            CheckPuzzle();
        }
    }

    public void CheckPuzzle()
    {
        int alignedCount = 0;
        Statue impossible = null;

        // Se busca el imposible
        foreach (var s in statues) {
            if(s.IsImpossible()){
                impossible = s;
                break;
            }
        }

        // Se cuenta los alineados
        foreach (var s in statues)
        {
            if (!s.IsImpossible() && s.IsAligned())
            {
                alignedCount++;
            }
        }

        // Si estan alineados todos menos 1
        if(alignedCount == statues.Length - 1)
        {
            PuzzleCompleted();
        }
    }
    public void PuzzleCompleted()
    {
        completed = true;
        door.SetActive(false); // Se abre la puerta (Para recoger la llave)
        Debug.Log("Felicidades");
    }
}
