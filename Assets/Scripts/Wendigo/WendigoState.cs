using UnityEngine;

public enum AIState
{
    /*
        0: Estado de espera
    */
    Idle,

    /*
        1: Reproduce la animación de grito
    */
    Scream,

    /*
        2: Movimiento libre por el mapa (Acecho)
    */
    Walk,

    /*
        3: Corre directamente a donde está el Player (Persecución)
    */
    Run,

    /*
        4: Inactivo y oculto (Fase Inicial)
    */
    Dormant
}
