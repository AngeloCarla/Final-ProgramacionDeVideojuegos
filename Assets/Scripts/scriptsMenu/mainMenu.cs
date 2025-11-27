using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Paneles del menú")]
    public GameObject mainMenu;
    public GameObject mainOpciones;
    public GameObject mainCreditos;

    // --- Botones del Main Menu ---
    public void Jugar()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void AbrirOpciones()
    {
        mainMenu.SetActive(false);
        mainOpciones.SetActive(true);
    }

    public void AbrirCreditos()
    {
        mainMenu.SetActive(false);
        mainCreditos.SetActive(true);
    }

    public void SalirOpciones()
    {
        mainOpciones.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SalirCreditos()
    {
        mainCreditos.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Salir del juego (solo funciona en build)");
    }
}
