using UnityEngine;

public class ActivePuzzle3 : MonoBehaviour
{
    [Header("Mision")]
    [SerializeField] private ControlPuzzleManager mission;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¿Crees poder controlarlo todo?");
            mission.gameObject.SetActive(true); // Activa la mision
            Destroy(gameObject); // Destruye para evitar repetirla
        }
    }
}
