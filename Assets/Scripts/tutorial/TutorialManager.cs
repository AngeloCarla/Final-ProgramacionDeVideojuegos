using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement player;
    public TutorialUI ui;

    public int step = 0;
    private int flashlightPresses = 0;
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
            step++;
            ShowStep();
            return;
        }

        if (step == 2)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                step++;
                ShowStep();
            }
        }
        if (step == 3)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                step++;
                ShowStep();
            }
        }
        if (step == 5)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlightPresses++;
                if (flashlightPresses >= 2) ui.Hide(); // Solo ocultamos UI, no avanzamos step
            }
        }
    }

    public void ShowStep()
    {
        switch (step)
        {
            case 0: // Intro Texto
                ui.ShowMessage("¿Dónde estoy...? No recuerdo nada... (Presiona cualquier tecla)", WaitForAnyKey);
                player.movementLocked = true;
                break;

            case 1: // Intro WASD Texto
                ui.ShowMessage("Necesito moverme... (Usa W A S D)", WaitForAnyKey);
                break;

            case 2: // Intro WASD Acción
                player.movementLocked = false;
                ui.Hide(); // Ocultamos texto para dejar jugar
                break;

            case 3: // Intro Correr Texto
                ui.ShowMessage("(Ahora intenta correr: SHIFT + W)", WaitForAnyKey);
                break;

            case 4:
                break;

            case 5: // Linterna Texto
                ui.ShowMessage("(Perfecto. Ahora probá tu linterna con F)", WaitForAnyKey);
                break;

            case 6: // INICIO ESCENA 1
                ui.ShowMessage("Y esto... ¿Qué es? (Utiliza E para interactuar)", WaitForAnyKey);
                break;

            case 7:
                ui.Hide();
                break;

            case 8: // INICIO ESCENA 2
                ui.ShowMessage("Uhm... Quizás si utilizo la linterna... ¿Será eso la respuesta?", null);
                break;

            case 9: // FINAL
                ui.ShowMessage("¿Qué clase de lugar es éste? (Fin del Tutorial)", EndTutorial);
                player.movementLocked = true;
                break;
        }
    }

    void WaitForAnyKey()
    {
        waitingForAnyKey = true;
    }

    void EndTutorial()
    {
        // Lógica de fin de juego o cargar nivel 1
        Debug.Log("Cargando Nivel 1...");
    }
}