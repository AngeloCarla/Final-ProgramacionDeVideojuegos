using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour, IInteractable
{
    public TutorialManager tutorialManager;
    public GameObject hiddenPassage;

    [Header("Conexión con Siguiente Escena")]
    public GameObject scene2Object;

    private int hitCount = 0;
    private bool surrendered = false;

    public void Interact()
    {
        ReceiveHit();
    }

    public void ReceiveHit()
    {
        if (surrendered) return;

        hitCount++;
        StartCoroutine(Vibrate());

        if (hitCount == 3)
        {
            // Bloqueamos movimiento para leer, luego activamos el chequeo de rendición
            tutorialManager.player.movementLocked = true;
            tutorialManager.ui.ShowMessage("Quizás... A veces soltar es avanzar...", StartSurrenderCheck);
        }
    }

    private void StartSurrenderCheck()
    {
        tutorialManager.player.movementLocked = false;
        surrendered = true;
    }

    void Update()
    {
        if (surrendered)
        {
            // Detectar caminar hacia atrás
            if (Input.GetAxisRaw("Vertical") < -0.1f)
            {
                StartCoroutine(RevealPassage());
                surrendered = false;
            }
        }
    }

    IEnumerator Vibrate()
    {
        // (Tu código de vibración original está bien aquí)
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < 0.2f)
        {
            float x = Random.Range(-0.1f, 0.1f);
            transform.position = originalPos + new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }

    IEnumerator RevealPassage()
    {
        // 1. Quitar el muro
        if (hiddenPassage != null) hiddenPassage.SetActive(false);

        // 2. ACTIVAR LA SIGUIENTE ESCENA (Si estaba oculta)
        if (scene2Object != null) scene2Object.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // 3. Destruir este objeto para limpiar
        Destroy(gameObject);
    }
}