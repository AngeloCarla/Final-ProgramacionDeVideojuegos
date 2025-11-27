using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    // Variable estática para acceder al DoorSystem desde cualquier script (patrón Singleton)
    public static DoorSystem Instance;

    [Header("Llaves")]
    [SerializeField] private bool[] keys = new bool[7]; // MAraca llaves que fueron recogidas
    private int keyCollected = 0; // Contador de llaves recolectadas
    void Awake()
    {
        // Evita que exista mas de un DoorSystem
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // No se destruye si cambia de escena
    }

    public void CollectedKey(int id)
    {
        if (id < 0 || id >= keys.Length) return;

        if (!keys[id])
        {
            keys[id] = true; // Marca en el array
            keyCollected++; // Suma al contador
        }

        Debug.Log("Tienes una llave");
    }
    public int KeyCollected { get { return keyCollected; } }
    public bool[] Keys => keys;
}
