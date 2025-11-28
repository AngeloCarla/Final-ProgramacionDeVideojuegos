using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialManager manager;
    public int targetStep = 6; // El paso donde inicia la escena 1

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && manager.step < targetStep)
        {
            manager.step = targetStep;
            manager.ShowStep();
            Destroy(gameObject); // Se autodestruye para no activarse de nuevo
        }
    }
}