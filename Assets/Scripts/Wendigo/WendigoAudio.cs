using UnityEngine;

public class WendigoAudio : MonoBehaviour
{
    [Header("Componentes")]
    private AudioSource audioSource;

    [Header("Audios")]
    public AudioClip screamClip;
    public AudioClip[] footstepClips;

    [Header("Configuración")]
    public float footstepInterval = 0.5f;
    private float nextFootstepTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1.0f;
    }

    /// <summary>
    /// Reproduce el sonido de grito. Llamado desde WendigoAI.cs
    /// </summary>
    public void PlayScream()
    {
        if (audioSource != null && screamClip != null)
        {
            audioSource.PlayOneShot(screamClip);
        }
    }

    /// <summary>
    /// Gestiona la reproducción de los pasos.
    /// </summary>
    public void HandleFootsteps(float speed)
    {
        // Solo reproduce si es el momento y hay clips de pasos
        if (Time.time > nextFootstepTime && footstepClips.Length > 0)
        {
            // La velocidad del intervalo se ajusta a la velocidad de movimiento
            // (a mayor velocidad, menor intervalo de tiempo)
            float interval = footstepInterval / Mathf.Max(1f, speed / 3f);
            nextFootstepTime = Time.time + interval;

            // Elegir un clip de paso aleatorio
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

            audioSource.PlayOneShot(clip);
        }
    }
}