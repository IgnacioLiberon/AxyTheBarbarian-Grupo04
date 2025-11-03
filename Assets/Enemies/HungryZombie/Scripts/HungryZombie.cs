using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HungryZombieFollow : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float stoppingDistance = 0.2f;

    [Header("Pathfinding (A*)")]
    [Tooltip("Cada cuánto recalcular el camino (segundos)")]
    [SerializeField] private float repathInterval = 0.25f;
    [Tooltip("Distancia para considerar alcanzado un waypoint")]
    [SerializeField] private float waypointTolerance = 0.08f;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Transform target;                  // Player
    private Vector2 desiredVelocity;

    // A*
    private AStarPathfinder pathfinder;        // tu componente de pathfinding
    private GridGraph grid;                    // grafo (para validar nodos)
    private readonly List<Vector3> path = new();
    private int currentWaypointIndex = 0;
    private float repathTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // Player por tag
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;

        // Referencias de A*
        pathfinder = FindFirstObjectByType<AStarPathfinder>();
        grid       = FindFirstObjectByType<GridGraph>();
    }

    private void Update()
    {
        if (target == null)
        {
            desiredVelocity = Vector2.zero;
            return;
        }

        // Distancia al jugador (centros)
        float dist = Vector2.Distance(transform.position, target.position);

        // Fuera de rango: quieto y limpiamos camino
        if (dist > detectionRadius)
        {
            desiredVelocity = Vector2.zero;
            path.Clear();
            currentWaypointIndex = 0;
            return;
        }

        // Cerca suficiente: quiet
        if (dist <= stoppingDistance)
        {
            desiredVelocity = Vector2.zero;
            path.Clear();
            currentWaypointIndex = 0;
        }
        else
        {
            // Recalcular ruta con temporizador (no cada frame)
            repathTimer -= Time.deltaTime;
            if (repathTimer <= 0f)
            {
                RecalculatePath();
                repathTimer = repathInterval;
            }

            MoveAlongPath();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = desiredVelocity;
    }

    private void RecalculatePath()
    {
        if (pathfinder == null)
        {
            path.Clear();
            path.Add(target.position);
            currentWaypointIndex = 0;
            return;
        }

        var newPath = pathfinder.FindPath(transform.position, target.position);

        path.Clear();
        if (newPath != null && newPath.Count > 0)
        {
            path.AddRange(newPath);
            currentWaypointIndex = 0;
        }
        else
        {
            // fallback recto si no hay ruta
            path.Add(target.position);
            currentWaypointIndex = 0;
        }
    }


    private void MoveAlongPath()
    {
        desiredVelocity = Vector2.zero;
        if (path.Count == 0 || currentWaypointIndex >= path.Count) return;

        // Salta todos los waypoints que ya están “casi alcanzados”
        float tol = Mathf.Max(waypointTolerance, stoppingDistance);
        while (currentWaypointIndex < path.Count &&
            Vector2.Distance(transform.position, path[currentWaypointIndex]) <= tol)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= path.Count) { desiredVelocity = Vector2.zero; return; }

        Vector3 wp = path[currentWaypointIndex];
        Vector2 toWp = (Vector2)(wp - transform.position);
        float dist = toWp.magnitude;

        if (dist > stoppingDistance)
        {
            Vector2 dir = toWp / Mathf.Max(dist, 0.0001f);
            desiredVelocity = dir * moveSpeed;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);

        // Dibuja camino actual
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.cyan;
            Vector3 prev = transform.position;
            for (int i = currentWaypointIndex; i < path.Count; i++)
            {
                Gizmos.DrawLine(prev, path[i]);
                Gizmos.DrawSphere(path[i], 0.05f);
                prev = path[i];
            }
        }
    }

    // Opcional: permitir setear desde spawner/director
    public void SetTarget(Transform t) => target = t;
}
