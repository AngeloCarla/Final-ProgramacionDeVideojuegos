using UnityEngine;

public class LookCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None
                ? CursorLockMode.Locked
                : CursorLockMode.None;

            Cursor.visible = (Cursor.lockState == CursorLockMode.None);
        }
    }
}
