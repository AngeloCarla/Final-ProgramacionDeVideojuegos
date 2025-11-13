using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelSceneTrigger : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    [SerializeField] private string sceneName = "nivel2";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador pasó por el túnel, cargando escena: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
