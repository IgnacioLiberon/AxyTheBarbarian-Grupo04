using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HungryZombie : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float stoppingDistance = 0.2f;

    [Header("Visual (opcional)")]
    [SerializeField] private bool flipSpriteByMovement = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Transform target;      // Player
    private Vector2 desiredVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // Busca al Player por tag. asi que el player debe tener tag "Player"
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;
    }

    private void Update()
    {
        if (target == null)
        {
            desiredVelocity = Vector2.zero;
            return;
        }

        Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
        float dist = toTarget.magnitude;

        if (dist > detectionRadius || dist <= stoppingDistance)
        {
            desiredVelocity = Vector2.zero;
        }
        else
        {
            Vector2 dir = toTarget / dist; // normalized
            desiredVelocity = dir * moveSpeed;
        }

        if (flipSpriteByMovement && spriteRenderer != null && Mathf.Abs(desiredVelocity.x) > 0.01f)
        {
            spriteRenderer.flipX = desiredVelocity.x < 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = desiredVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

    // Opcional: permitir setear el objetivo desde un spawner/director
    public void SetTarget(Transform t) => target = t;
}
