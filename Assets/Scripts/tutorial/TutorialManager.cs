using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement player;
    public TutorialUI ui;

    private int step = 0;
    private int flashlightPresses = 0;

    private bool waitingForAnyKey = false;

    void Start()
    {
        player.movementLocked = true;
        ShowStep();
    }

    void Update()
    {
        // Esperar "cualquier tecla" para avanzar
        if (waitingForAnyKey && Input.anyKeyDown)
        {
            waitingForAnyKey = false;
            step++;
            ShowStep();
            return;
        }

        // PASO 1 — aprender WASD
        if (step == 2)
        {
            if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D))
            {
                step++;
                ShowStep();
            }
        }

        // PASO 2 — correr con SHIFT + W
        if (step == 4)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                step++;
                ShowStep();
            }
        }

        // PASO 3 — Linterna con F (2 veces)
        if (step == 6)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlightPresses++;
                if (flashlightPresses >= 2)
                {
                    step++;
                    ShowStep();
                }
            }
        }
    }

    void ShowStep()
    {
        switch (step)
        {
            case 0:
                ui.ShowMessage("¿Dónde estoy...? No recuerdo nada...", WaitForAnyKey);
                player.movementLocked = true;
                break;

            case 1:
                ui.ShowMessage("Necesito moverme... Usa W A S D para desplazarte", WaitForAnyKey);
                break;

            case 2:
                player.movementLocked = false; 
                break;

            case 3:
                ui.ShowMessage("Bien. Ahora intenta correr: SHIFT + W", WaitForAnyKey);
                break;

            case 4:
                break;

            case 5:
                ui.ShowMessage("Perfecto. Ahora probá tu linterna con F", WaitForAnyKey);
                break;

            case 6:
                break;

            case 7:
                ui.ShowMessage("Tutorial completo. Buena suerte...", EndTutorial);
                player.movementLocked = true;
                break;

            case 8:
                player.movementLocked = false;
                ui.Hide();
                break;
        }
    }

    void WaitForAnyKey()
    {
        waitingForAnyKey = true;
    }

    void EndTutorial()
    {
        waitingForAnyKey = true;
    }
}
