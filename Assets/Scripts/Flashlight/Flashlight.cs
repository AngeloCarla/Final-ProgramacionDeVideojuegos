using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Linterna")]
    [SerializeField] private GameObject flashlight;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            LightManager();
        }
    }

    public void LightManager()
    {
        flashlight.SetActive(!flashlight.activeSelf);
    }
}
