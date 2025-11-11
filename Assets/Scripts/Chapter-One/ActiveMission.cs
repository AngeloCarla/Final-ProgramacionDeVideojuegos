using UnityEngine;

public class ActiveMission : MonoBehaviour
{
    [SerializeField] private CollectWoodMission mission;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mision: Busca la manera de cruzar");

            mission.EnableBrokenBridge();
            Destroy(gameObject); // Destruye para evitar repetirla
        }
    }
}
