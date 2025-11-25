using UnityEngine;

public class SoberbiaPuzzle : MonoBehaviour
{
    [Header("Lámparas del puzzle")]
    public LampController[] lamparas;

    [Header("Puerta que bloquea o desbloquea el camino")]
    public GameObject puerta;

    void Start()
    {
        // La puerta empieza bloqueando el camino
        if (puerta != null)
            puerta.SetActive(true);
    }

    void Update()
    {
        bool algunaPrendida = false;

        // Si alguna lámpara está prendida → la puerta debe bloquear
        foreach (var lamp in lamparas)
        {
            if (lamp.IsOn())
            {
                algunaPrendida = true;
                break;
            }
        }

        if (algunaPrendida)
        {
            // Mantener la puerta visible/cerrada
            if (puerta != null)
                puerta.SetActive(true);
        }
        else
        {
            // Todas apagadas → ocultar la puerta
            if (puerta != null)
                puerta.SetActive(false);
        }
    }
}
