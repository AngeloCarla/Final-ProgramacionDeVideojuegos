using UnityEngine;

public class BridgeMission : MonoBehaviour
{
    [Header("Recoleccion")]
    [SerializeField] private GameObject[] woods; // Madera
    [SerializeField] private CollectWoodMission mission; // Mision

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objetivo: Recolecta madera para recostruir el puente");

            // Activa cada trozo de madera en el array
            foreach (GameObject wood in woods)
            {
                wood.SetActive(true);
            }

            mission.StartCollectionPhase();

            // Destruye el trigger para que no se vuelva a activar
            Destroy(gameObject);
        }
    }
}
