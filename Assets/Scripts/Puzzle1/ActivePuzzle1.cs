using UnityEngine;

public class ActivePuzzle1 : MonoBehaviour
{
    [Header("Mision")]
    [SerializeField] private AmbitionPuzzleManager mission;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¿De verdad te basta con esto?");

          //  mission.gameObject.SetActive(true); // Activa la mision
            Destroy(gameObject); // Destruye para evitar repetirla
        }
    }
}
