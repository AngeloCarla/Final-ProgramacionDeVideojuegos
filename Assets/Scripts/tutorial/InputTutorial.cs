using UnityEngine;

public class InputTutorial : MonoBehaviour
{
    [Header("Referencias")]
    public TutorialManager manager; 

    private int flashlightPresses = 0;

    void Update()
    {
        if (manager.IsWaitingForTextKey()) return;

        // --- PASO 2: WASD ---
        if (manager.step == 2)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                manager.AdvanceStep();
            }
        }

        // --- PASO 3: CORRER (SHIFT + W) ---
        if (manager.step == 3)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                manager.AdvanceStep();
            }
        }

        // --- PASO 5: LINTERNA ---
        if (manager.step == 5)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlightPresses++;
                if (flashlightPresses >= 2)
                {
                    manager.ui.Hide();
                }
            }
        }
    }
}