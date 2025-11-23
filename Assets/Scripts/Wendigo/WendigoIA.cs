using UnityEngine;
using UnityEngine.AI;

public class WendigoAI : MonoBehaviour
{
    // --- COMPONENTES ---
    private NavMeshAgent agent;
    private Animator animator;
    private WendigoAudio wendigoAudio;

    private Transform Player;
    private CollectWoodMission gameManager;

    // Animator Hash
    private readonly int isWalking = Animator.StringToHash("isWalking");
    private readonly int doScream = Animator.StringToHash("Scream");
    private readonly int isRunning = Animator.StringToHash("isRunning");

    [Header("Movimiento y Velocidades")]
    public float patrolSpeed = 1.5f;
    public float runSpeed = 6f;
    public float safeDistance = 30f;
    public float patrolRange = 50f;

    [Header("Tiempos de Comportamiento")]
    public float idleTimeMin = 3f;
    public float idleTimeMax = 6f;
    public float screamDelay = 1.5f;

    [Header("Lógica Interna")]
    public float stopDistance = 1.5f;

    private Vector3 walkTarget;
    public AIState currentState = AIState.Dormant;
    private float stateTime;

    private readonly string PlayerTag = "Player";

    // ============================================================
    //   CAMBIO DE ESTADO
    // ============================================================

    public void SetState(AIState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        // Reiniciar animaciones
        animator.SetBool(isWalking, false);
        animator.SetBool(isRunning, false);

        // CONTROL DEL NAVMESH
        if (agent != null)
            agent.isStopped = true;

        switch (newState)
        {
            case AIState.Dormant:
                gameObject.SetActive(false);
                break;

            case AIState.Walk:
                gameObject.SetActive(true);

                // Evita errores de NavMesh
                if (!EnsureOnNavmesh()) return;

                animator.SetBool(isWalking, true);

                agent.speed = patrolSpeed;
                agent.isStopped = false;

                GetNewPatrolPoint(safeDistance);
                agent.SetDestination(walkTarget);
                break;

            case AIState.Idle:
                animator.SetBool(isWalking, false);
                if (agent != null) agent.isStopped = true;
                stateTime = Time.time + Random.Range(idleTimeMin, idleTimeMax);
                break;

            case AIState.Scream:
                if (agent != null) agent.isStopped = true;
                animator.SetTrigger(doScream);

                if (wendigoAudio != null)
                    wendigoAudio.PlayScream();

                stateTime = Time.time + screamDelay;
                break;

            case AIState.Run:
                gameObject.SetActive(true);

                if (!EnsureOnNavmesh()) return;

                animator.SetBool(isRunning, true);

                if (wendigoAudio != null)
                    wendigoAudio.PlayScream();

                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }
    }

    // ============================================================
    //   LÓGICA DE ESTADOS
    // ============================================================

    private void HandleStateLogic()
    {
        switch (currentState)
        {
            case AIState.Walk:
                WalkLogic();
                break;

            case AIState.Idle:
                if (Time.time >= stateTime)
                    SetState(AIState.Scream);
                break;

            case AIState.Scream:
                if (Time.time >= stateTime)
                    SetState(AIState.Walk);
                break;

            case AIState.Run:
                if (Player != null && agent.isOnNavMesh)
                    agent.SetDestination(Player.position);
                break;
        }
    }

    // ============================================================
    //   MONOBEHAVIOUR
    // ============================================================

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        wendigoAudio = GetComponent<WendigoAudio>();

        gameManager = FindFirstObjectByType<CollectWoodMission>();
        if (gameManager != null)
            Player = gameManager.PlayerTransform;

        // --- Asegura que CAIGA sobre el NavMesh ---
        EnsureOnNavmesh();

        SetState(AIState.Dormant);
    }

    private void Update()
    {
        if (currentState == AIState.Dormant)
            return;

        // Evita mil errores de NavMesh por 1 frame
        if (!agent.isOnNavMesh)
            return;

        HandleStateLogic();

        // Sonido de pasos en Walk y Run
        if (wendigoAudio != null)
        {
            if (currentState == AIState.Walk || currentState == AIState.Run)
                wendigoAudio.HandleFootsteps(agent.velocity.magnitude);
        }
    }

    // ============================================================
    //   COLISIÓN (GAME OVER)
    // ============================================================

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == AIState.Run &&
            other.CompareTag(PlayerTag))
        {
            if (agent != null) agent.isStopped = true;
            animator.SetBool(isRunning, false);

            if (gameManager != null)
                gameManager.YouLose();
        }
    }

    // ============================================================
    //   LÓGICA DE MOVIMIENTO
    // ============================================================

    private void WalkLogic()
    {
        if (!agent.isOnNavMesh)
            return;

        if (!agent.pathPending && agent.remainingDistance <= stopDistance)
        {
            SetState(AIState.Idle);
        }
    }

    private void GetNewPatrolPoint(float minDistance = 0f)
    {
        Vector3 randomDirection;
        Vector3 candidate;
        int attempts = 0;

        do
        {
            randomDirection = Random.insideUnitSphere * patrolRange;
            candidate = transform.position + randomDirection;
            attempts++;

        } while (Vector3.Distance(candidate, Player.position) < minDistance &&
                 attempts < 10);

        walkTarget = new Vector3(candidate.x, transform.position.y, candidate.z);

        if (agent.isOnNavMesh)
            agent.SetDestination(walkTarget);
    }

    // ============================================================
    //   UTILIDAD: asegurar que el agente esté sobre un NavMesh válido
    // ============================================================

    private bool EnsureOnNavmesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 3f, NavMesh.AllAreas))
        {
            transform.position = hit.position;

            // Para evitar que se quede "desactivado"
            if (!agent.isOnNavMesh)
            {
                agent.Warp(hit.position);
            }

            return true;
        }

        Debug.LogError("Wendigo no puede colocarse sobre el NavMesh.");
        return false;
    }
}