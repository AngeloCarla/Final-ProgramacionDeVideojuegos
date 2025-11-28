using UnityEngine;
public class Trigger : MonoBehaviour
{
    public TutorialManager manager;
    public int targetStep = 8;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && manager.step < targetStep)
        {
            manager.step = targetStep;
            manager.ShowStep();
            Destroy(gameObject);
        }
    }
}