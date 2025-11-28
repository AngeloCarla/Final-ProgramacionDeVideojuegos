using UnityEngine;

public class FalseWall : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Feedback nulo o sutil
        Debug.Log("Parece que falta algo... o quizás no encaja.");

        // Aquí podrías poner un sonido de "Locked" (puerta cerrada)
        // o una pequeña vibración, pero nada que indique progreso.
        transform.position += Vector3.right * 0.05f;
        Invoke(nameof(ResetPos), 0.1f);
    }

    void ResetPos()
    {
        transform.position -= Vector3.right * 0.05f;
    }
}