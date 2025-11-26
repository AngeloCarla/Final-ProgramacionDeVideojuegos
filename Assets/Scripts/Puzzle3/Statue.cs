using System.Collections;
using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [Header("Manager")]
    [SerializeField] private ControlPuzzleManager manager; // Manager del puzzle

    private float rotationStep = 30f; // Cuanto gira la pieza
    private float correctAngle = 0f; // Angulo correcto

    private bool autoAligning = false;

    public void RotateStatue()
    {
        // Si se está auto–alineando, no se puede girar
        if (autoAligning) return;

        transform.Rotate(0, rotationStep, 0); // Rota la pieza

        manager.OnPlayerInteraction(); // Llama cada vez que interactua (para el timeout)
    }

    public void AutoAlign()
    {
        autoAligning = true; // Bloquea interaccion
        StartCoroutine(AutoAlightRoutine());
    }

    public IEnumerator AutoAlightRoutine()
    {
        float speed = 2f; // /Velocidad con la que se ajusta la estatua
        float target = correctAngle; // Angulo objetivo

        // Mientras no este cerca del angulo correcto
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, target)) > 0.05f)
        {
            // Suaviza la rotacion
            float y = Mathf.LerpAngle(transform.eulerAngles.y, target, Time.deltaTime * speed);
            transform.eulerAngles = new Vector3(0, y, 0); // Aplica la rotacion
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, target, 0); // Deja exacto el angulo
    }

    public void Interact()
    {
        RotateStatue();
    }
}
