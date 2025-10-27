using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public Transform launchPoint;
    public int arrowsPerAttack = 3;
    public Vector2 directionMin = new Vector2(0f, -1f);
    public Vector2 directionMax = new Vector2(1f, 0f); 

    [Header("Timing Settings")]
    public float throwCooldown = 2f;
    private float stateTimer;

    private void Start()
    {
        stateTimer = throwCooldown;
    }

    private void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateState();
    }

    private void UpdateState()
    {
        if (stateTimer <= 0f)
        {
            ShootArrows();
            stateTimer = throwCooldown;
        }
    }

    private void ShootArrows()
    {
        for (int i = 0; i < arrowsPerAttack; i++)
        {
            ThrowArrow();
        }
    }

    private void ThrowArrow()
    {
        if (arrowPrefab == null || launchPoint == null) return;

        GameObject arrowObject = Instantiate(arrowPrefab, launchPoint.position, launchPoint.rotation);
        Arrow arrowScript = arrowObject.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            Vector2 throwDirection = new Vector2(
                Random.Range(directionMin.x, directionMax.x),
                Random.Range(directionMin.y, directionMax.y)
            );
            arrowScript.SetDirection(throwDirection);
        }
    }

    //TODO
    //Velocity is calculated in the Controller, and movement in the Physics
}
