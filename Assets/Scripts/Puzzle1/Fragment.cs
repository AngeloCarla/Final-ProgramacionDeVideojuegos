using UnityEngine;

public class Fragment : MonoBehaviour, IInteractable
{
    // Script base para cualquier objeto que el jugador pueda recoger o eliminar del escenario 
    [Header("Manager")]
    // [SerializeField] private bool isExtra = false;

    [SerializeField] private AmbitionPuzzleManager puzzle1;
    void Start()
    {
        puzzle1 = FindAnyObjectByType<AmbitionPuzzleManager>();
    }

    public void PickUpFragment()
    {
        puzzle1.AddFragment();
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUpFragment();
    }
}
