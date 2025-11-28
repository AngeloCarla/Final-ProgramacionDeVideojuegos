using UnityEngine;
using System.Collections;

public class Scene2 : MonoBehaviour
{
    #region Referencias
    public TutorialManager tutorialManager;
    public GameObject exitDoor;
    public Flashlight flashlight;

    [Header("Ajustes de Detección")]
    public float requiredFocusTime = 2.5f;
    public float maxMovementThreshold = 0.05f;

    private float focusTimer = 0f;
    private bool isDissolving = false;
    private Renderer shadowRenderer;
    private CharacterController playerCC;
    public int nextTutorialStep = 9;
    #endregion

    #region Lógica del puzle
    void Start()
    {
        shadowRenderer = GetComponent<Renderer>();
        if (shadowRenderer == null)
        {
            Debug.LogError("ShadowDissolver requiere un componente Renderer.");
            enabled = false;
        }

        playerCC = tutorialManager.player.GetComponent<CharacterController>();

        // Bloqueamos la puerta hasta que se resuelva la escena
        if (exitDoor != null) exitDoor.SetActive(false);
    }
    
    void Update()
    {
        if (isDissolving) return;

        // 1. Verificar si la Linterna está Encendida
        bool isLightOn = flashlight.gameObject.activeSelf;

        // 2. Verificar si está Apuntando a las Sombras (Raycast)
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        bool isPointing = Physics.Raycast(ray, out hit, 100f) && hit.transform == transform;

        // 3. Verificar Quietud (Usando la velocidad del CharacterController)
        float playerSpeed = playerCC.velocity.magnitude;
        bool isQuiet = playerSpeed < maxMovementThreshold;

        if (isLightOn && isPointing && isQuiet)
        {
            // La condición se cumple: empezar a contar
            focusTimer += Time.deltaTime;

            if (focusTimer >= requiredFocusTime)
            {
                // ¡Gesto Clave Completado!
                StartDissolve();
            }
        }
        else
        {
            // Si cualquier condición falla, reseteamos el temporizador
            if (focusTimer > 0)
            {
                focusTimer -= Time.deltaTime * 2;
                if (focusTimer < 0) focusTimer = 0;
            }
        }
    }
    #endregion

    #region Lógica de desvanecimiento
    private void StartDissolve()
    {
        isDissolving = true;

        // 1. Disolver el objeto visualmente (usando una Corrutina)
        StartCoroutine(DissolveAndAdvance());

        // 2. Mostrar texto de progreso
        tutorialManager.ui.ShowMessage("Tú eliges cómo te ves.", null);
    }

    IEnumerator DissolveAndAdvance()
    {
        float duration = 2.0f;
        float elapsed = 0f;

        Color startColor = shadowRenderer.material.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / duration);

            Color newColor = startColor;
            newColor.a = alpha;
            shadowRenderer.material.color = newColor;

            yield return null;
        }

        gameObject.SetActive(false);

        if (exitDoor != null) exitDoor.SetActive(true);

        // Avanza al paso final
        tutorialManager.step = nextTutorialStep;
        tutorialManager.ShowStep();
    }
    #endregion
}