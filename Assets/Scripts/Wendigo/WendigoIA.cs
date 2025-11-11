using UnityEngine;
using UnityEngine.AI;

public class WendigoAI : MonoBehaviour
{
    #region Components & References
    // Componentes obligatorios
    private NavMeshAgent agent; // << CORREGIDO: Usamos 'agent'
    private Animator animator;
    private WendigoAudio wendigoAudio;

    // Referencia al jugador
    private Transform Player;

    // Parámetros de Animator
    private readonly int isWalking = Animator.StringToHash("IsWalking");
    private readonly int doScream = Animator.StringToHash("Scream");
    #endregion

    #region Configuración
    [Header("1. Movimiento y Rango")]
    public float patrolSpeed = 1.5f;
    public float evadeSpeed = 2.5f;
    [Tooltip("Radio máximo de búsqueda para puntos de patrullaje")]
    public float patrolRange = 50f;

    [Header("2. Tiempos de Comportamiento")]
    [Tooltip("Tiempo mínimo y máximo que el Wendigo espera en estado Idle")]
    public float idleTimeMin = 3f;
    public float idleTimeMax = 6f;
    [Tooltip("Delay después del grito antes de volver a caminar")]
    public float screamDelay = 1.5f;

    [Header("3. Detección y Evasión")]
    [Tooltip("Radio en el que detecta al jugador y comienza a evadir")]
    public float detectionRadius = 15f;
    [Tooltip("Distancia que intenta mantener lejos del jugador al evadir")]
    public float evadeDistance = 20f;
    #endregion

    #region Máquina de Estados (Lógica Interna)
    private AIState currentState = AIState.Walk;
    private float stateTime; // Usado para temporizar estados (Idle, Scream)
    private bool isEvading = false; // Cambia el comportamiento dentro de Walk

    private void SetState(AIState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        // --- Inicialización del nuevo estado ---
        switch (newState)
        {
            case AIState.Walk:
                agent.isStopped = false; // << CORREGIDO: Usa 'agent'
                agent.speed = patrolSpeed; // << CORREGIDO: Usa 'agent'
                GetNewPatrolPoint();
                animator.SetBool(isWalking, true);
                break;

            case AIState.Idle:
                agent.isStopped = true; // << CORREGIDO: Usa 'agent'
                animator.SetBool(isWalking, false);
                stateTime = Time.time + Random.Range(idleTimeMin, idleTimeMax);
                break;

            case AIState.Scream:
                agent.isStopped = true; // << CORREGIDO: Usa 'agent'
                animator.SetTrigger(doScream); // Activa la animación
                wendigoAudio.PlayScream();    // Activa el audio
                stateTime = Time.time + screamDelay;
                break;
        }
    }

    private void HandleStateLogic()
    {
        switch (currentState)
        {
            case AIState.Walk:
                WalkLogic();
                break;

            case AIState.Idle:
                if (Time.time >= stateTime)
                {
                    SetState(AIState.Scream);
                }
                break;

            case AIState.Scream:
                if (Time.time >= stateTime)
                {
                    SetState(AIState.Walk);
                }
                break;
        }
    }
    #endregion

    #region Monobehaviour Core
    private void Awake()
    {
        // Obtención de componentes
        agent = GetComponent<NavMeshAgent>(); // << CORREGIDO: Asigna a 'agent'
        animator = GetComponent<Animator>();
        wendigoAudio = GetComponent<WendigoAudio>();

        // Buscar al jugador (asumiendo Tag: "Player")
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj.transform;
        }

        SetState(AIState.Walk);
    }

    private void Update()
    {
        // 1. Detección Global
        if (Player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            // Si detecta al jugador Y está en estado de movimiento
            if (distanceToPlayer < detectionRadius && currentState == AIState.Walk)
            {
                isEvading = true;
            }
            // Si el jugador se va Y está en modo evasión
            else if (distanceToPlayer > detectionRadius)
            {
                isEvading = false;
            }
        }

        // 2. Ejecución de la lógica del estado actual
        HandleStateLogic();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    #endregion

    #region Lógica de Movimiento
    private void WalkLogic()
    {
        if (isEvading)
        {
            // --- Evasión ---
            agent.speed = evadeSpeed; // << CORREGIDO: Usa 'agent'

            // Calcula la dirección opuesta al jugador
            Vector3 directionFromPlayer = transform.position - Player.position;
            Vector3 evadeTarget = transform.position + directionFromPlayer.normalized * evadeDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(evadeTarget, out hit, evadeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position); // << CORREGIDO: Usa 'agent'
            }
        }
        else
        {
            // --- Patrullaje (Deambulación) ---
            agent.speed = patrolSpeed; // << CORREGIDO: Usa 'agent'

            // Si el agente llegó a su destino, cambia a Idle
            if (!agent.pathPending && agent.remainingDistance < 1f) // << CORREGIDO: Usa 'agent'
            {
                SetState(AIState.Idle);
            }
            // Pequeño chequeo adicional por si se queda atascado en el destino
            else if (agent.remainingDistance <= 1f && !agent.hasPath) // << CORREGIDO: Usa 'agent'
            {
                GetNewPatrolPoint();
            }
        }
    }

    private void GetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;
        NavMeshHit hit;

        // Encuentra el punto más cercano válido en el NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // << CORREGIDO: Usa 'agent'
        }
    }

    #endregion
}