using UnityEngine;
using UnityEngine.UIElements;

public class InteractionSystem : MonoBehaviour
{
    LayerMask mask; // Capa que define los objetos a detectar
    private float dist = 8f; // Distancia maxima para interacuar
    private Transform currentSelection; // Ultimo objeto detectado

    void Start()
    {
        // Solo detecta los objetos que estén asignados a la capa "RaycastDetect"
        mask = LayerMask.GetMask("RaycastDetect");
    }

    void Update()
    {
        // Crea un rayo desde la camara principal hacia la posicion del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // Guarda informacion del objeto golpeado por el rayo

        // Lanza el raycast: (origen, dirección, salida del impacto, distancia, capa)
        if (Physics.Raycast(ray, out hit, dist, mask))
        {
            // Todo objeto interactuable debe heredar la interfaz IInteractable
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            // Si se apunta a un nuevo objeto, actualiza la seleccion
            if (hit.transform != currentSelection)
            {
                DeselectCurrent();
                SelectObject(hit.transform, interactable);
            }

            // --- Interaccion ---
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable != null)
                {
                    interactable.Interact(); // Ejecuta la accion
                    DeselectCurrent();
                }
            }
        }
        else
        {
            DeselectCurrent(); // Si no golpea nada, se limpia la seleccion
        }
    }

    void SelectObject(Transform transform, IInteractable interactable)
    {
        currentSelection = transform;

        // Muestra en la consola el nombre del objeto al que se esta apuntando
        // (Mientras este en el layer RayCastDetect)
        if (interactable != null)
        {
            Debug.Log(transform.name);
        }
    }
    void DeselectCurrent()
    {
        if (currentSelection != null)
        {
            Debug.Log("");
            currentSelection = null;
        }
    }
}