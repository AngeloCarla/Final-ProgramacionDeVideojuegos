using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement player;
    public TutorialUI ui;

    public int step = 0;
    private bool waitingForAnyKey = false;

    void Start()
    {
        player.movementLocked = true;
        ShowStep();
    }

    void Update()
    {
        if (waitingForAnyKey && Input.anyKeyDown)
        {
            waitingForAnyKey = false;
            AdvanceStep();
        }
    }

    // --- FUNCIONES PÚBLICAS (Para que otros scripts las llamen) ---

    public void AdvanceStep()
    {
        step++;
        ShowStep();
    }

    public bool IsWaitingForTextKey()
    {
        return waitingForAnyKey;
    }

    // --- MÁQUINA DE ESTADOS ---

    public void ShowStep()
    {
        Debug.Log("Tutorial Paso: " + step);

        switch (step)
        {
            // --- INTRODUCCIÓN ---
            case 0:
                ui.ShowMessage("¿Dónde estoy...? No recuerdo nada... (Presiona tecla)", WaitForAnyKey);
                player.movementLocked = true;
                break;

            case 1:
                ui.ShowMessage("Necesito moverme... (Usa W A S D)", WaitForAnyKey);
                break;

            case 2:
                player.movementLocked = false;
                ui.Hide();
                break;

            case 3:
                ui.ShowMessage("(Ahora intenta correr: SHIFT + W)", null);
                break;

            case 4:
                ui.ShowMessage("(Perfecto. Ahora probá tu linterna con F)", WaitForAnyKey);
                break;

            case 5: 
                ui.Hide();
                break;

            // --- ESCENAS DEL JUEGO ---

            case 6:
                ui.ShowMessage("Y esto... ¿Qué es? (Utiliza E para interactuar)", WaitForAnyKey);
                break;

            case 7:
                ui.Hide();
                break;

            case 8:
                ui.ShowMessage("Uhm... Quizás si utilizo la linterna... ¿Será eso la respuesta?", null);
                break;

            case 9:
                ui.ShowMessage("¿Qué clase de lugar es éste? (Fin del Tutorial)", EndTutorial);
                break;
        }
    }

    void WaitForAnyKey()
    {
        waitingForAnyKey = true;
    }

    void EndTutorial()
    {
        Debug.Log("Llamando al LevelManager para cargar Nivel 1...");
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadNextLevel();
        }
        else
        {
            Debug.LogError("ERROR: LevelManager no está presente en la escena. No se pudo cargar el siguiente nivel.");
        }
    }
}