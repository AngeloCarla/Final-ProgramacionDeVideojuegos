using System.Collections;
using UnityEngine;

public class AmbitionPuzzleManager : MonoBehaviour
{
    [Header("Fragmento y puerta")]
    [SerializeField] private GameObject[] extraFragments; // Fragmentos extra
    [SerializeField] private GameObject door; // Puerta donde esta la llave

    private int requiredFragments = 3; // Fragmentos que debe recoger
    private int collectedFragments = 0; // Fragmentos recolectados

    private bool completed = false; // Completa correctamente el puzzle
    private bool corrupted = false; // Se corrompe (no cumple)

    public void AddFragment(bool isExtra)
    {
        // Si son fragmentos extra, el juego se corrompe
        if (isExtra)
        {
            corrupted = true;
            return;
        }

        collectedFragments++; // Recolecta fragmentos

        if (collectedFragments == requiredFragments && !completed) // Si llega al total
        {
            completed = true;
            StartCoroutine(OpenDoor()); // Se abre la puerta
        }
    }

    private IEnumerator OpenDoor()
    {
        // Activacion de fragmentos extra (uno a uno)
        foreach (var f in extraFragments)
        {
            float t = 3f; // Tiempo antes de mostrar otro fragmento
            while (t > 0)
            {
                t -= Time.deltaTime;
                yield return null; // Pasa al siguiente frame
            }

            //if (corrupted) break; // Si se corrompe, detiene todo

            f.SetActive(true);
        }

        // Espera hasta el final para decidir el destino del personaje
        float finalWait = 6f;
        while (finalWait > 0)
        {
            finalWait -= Time.deltaTime;
            yield return null; // Pasa al siguiente frame
        }

        // Si nunca se corrompio
        if (!corrupted)
        {
            PuzzleCompleted(); // Completa el puzzle
        }
        else // Si cae en la tentacion
        {
            Debug.Log("Tomaste mas de lo que debias");
            yield return new WaitForSeconds(6f);
            Debug.Log("La ambicion te consumio...");

            // --- CASTIGO ---
        }
    }

    public void PuzzleCompleted()
    {
        completed = true;
        door.SetActive(false); // Se abre la puerta (Para recoger la llave)
        Debug.Log("Felicidades");
    }
}
