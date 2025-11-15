using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    LayerMask mask; // Capa que define los objetos a detectar
    private float dist = 5f; // Distancia maxima para interacuar
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
            var pickUp = hit.collider.GetComponent<PickUpObject>(); // Busca el componente para recoger
            var delivery = hit.collider.GetComponent<WoodDelivery>(); // Busca si es para entregar 
            var rotating = hit.collider.GetComponent<RotatingPiece>(); // Si rota la pieza
            var torch = hit.collider.GetComponent<Torch>(); // Antorcha

            // Si se apunta a un nuevo objeto, actualiza la seleccion
            if (hit.transform != currentSelection)
            {
                DeselectCurrent();
                SelectObject(hit.transform, pickUp, delivery, rotating, torch);
            }

            // --- Interaccion ---
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                if (pickUp != null)
                {
                    pickUp.PickUp(); // Ejecuta la accion de recoger
                    DeselectCurrent();
                }
                else if (delivery != null)
                {
                    delivery.RepairBridge(); // Ejecuta la accion al entregar
                    DeselectCurrent();
                }
                else if (rotating != null)
                {
                    rotating.Interact();
                    DeselectCurrent();
                }
                else if (torch != null)
                {
                    torch.Interact();
                    DeselectCurrent();
                }
            }
        }
        else
        {
            DeselectCurrent(); // Si no golpea nada, se limpia la seleccion
        }
    }

    void SelectObject(Transform transform, PickUpObject pickUp, WoodDelivery delivery, RotatingPiece rotating, Torch torch)
    {
        currentSelection = transform;

        // Muestra en la consola el nombre del objeto al que se esta apuntando
        // (Mientras este en el layer RayCastDetect)
        if (pickUp != null)
        {
            Debug.Log($"{pickUp.objectName}");
        }
        else if (delivery != null)
        {
            Debug.Log($"{delivery.gameObject.name}");
        }
        else if (rotating != null)
        {
            Debug.Log($"{rotating.gameObject.name}");
        }
        else if (torch != null)
        {
            Debug.Log($"{torch.gameObject.name}");
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
