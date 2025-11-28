using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas
using System.Collections; // Necesario para Corrutinas

public class LevelManager : MonoBehaviour
{
    // --- SINGLETON PATTERN ---
    public static LevelManager Instance;

    [Header("Escenas del Juego")]
    public string tutorialSceneName = "Tutorial";
    public string level1SceneName = "Nivel1";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- FUNCIONES PÚBLICAS DE CARGA ---

    /// <summary>
    /// Carga la escena principal del Nivel 1 (usada por TutorialManager al final).
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadAsync(level1SceneName));
    }

    // --- CORRUTINA DE CARGA ASINCRÓNICA ---

    IEnumerator LoadAsync(string sceneName)
    {
        // 1. Inicia la operación de carga en segundo plano
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Cargando: " + (progress * 100f) + "%");

            yield return null;
        }
    }
}