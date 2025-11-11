using UnityEngine;

public class WendigoAudio : MonoBehaviour
{
    #region Componentes & Referencias

    private AudioSource audioSource;

    #endregion

    #region Configuración

    [Header("Clips de Audio")]
    public AudioClip screamSound;
    public AudioClip footstepSound;

    [Header("Configuración de Grito")]
    [Tooltip("Tiempo inicial antes del primer grito")]
    public float initialScreamDelay = 10f;
    [Tooltip("Tiempo base entre gritos automáticos")]
    public float baseScreamInterval = 30f;

    #endregion

    #region Lógica de Grito
    private void PerformScream()
    {
        // 1. Ejecutar el audio
        PlayScreamAudioOnly();

        // 2. Determinar un nuevo intervalo con aleatoriedad
        float randomDelay = Random.Range(0f, 15f);
        float nextInterval = baseScreamInterval + randomDelay;

        // 3. Reiniciar el ciclo con el nuevo tiempo aleatorio
        Invoke("PerformScream", nextInterval);
    }

    private void PlayScreamAudioOnly()
    {
        if (screamSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(screamSound);
        }
    }
    #endregion

    #region Monobehaviour Core
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Invoke("PerformScream", initialScreamDelay);
    }
    #endregion

    #region Funciones de Reproducción (Llamadas Externamente)
    public void PlayFootstep()
    {
        if (footstepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    public void PlayScream()
    {
        // 1. Si WendigoAI.cs quiere que grite, cancelamos el ciclo automático.
        CancelInvoke("PerformScream");

        // 2. Reproducimos el grito forzado
        PlayScreamAudioOnly();

        // 3. Reiniciamos el ciclo automático.
        // Usa Invoke("PerformScream"...) para reanudar el ciclo recursivo.
        Invoke("PerformScream", baseScreamInterval);
    }
    #endregion
}