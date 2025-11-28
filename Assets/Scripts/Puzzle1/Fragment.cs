using UnityEngine;

public class Fragment : MonoBehaviour, IInteractable
{
    // Script base para cualquier objeto que el jugador pueda recoger o eliminar del escenario 
    [Header("Fragmento Extra")]
   // [SerializeField] private bool isExtra = false;

    private AmbitionPuzzleManager puzzle1;
    void Start()
    {
        puzzle1 = FindAnyObjectByType<AmbitionPuzzleManager>();
    }

    public void PickUpFragment()
    {
        /*
        if (puzzle1 != null)
        {
            puzzle1.AddFragment(isExtra);
        }
        */

        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUpFragment();
    }
}
