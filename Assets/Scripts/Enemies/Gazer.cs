using UnityEngine;

public class Gazer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Vector2 offsetTop = new Vector2(0f, 3f);
    public Vector2 offsetBottom = new Vector2(0f, -3f);

    private Vector2 topPos;
    private Vector2 bottomPos;
    private Vector2 startPos;

    private bool goingUp = false;
    private float tolerance = 0.05f;

    private void Start()
    {
        startPos = transform.position;
        topPos = startPos + offsetTop;
        bottomPos = startPos + offsetBottom;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        Vector2 target = goingUp ? topPos : bottomPos;
        MoveTo(target);
        ChangeDirection(target);
    }

    private void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void ChangeDirection(Vector2 target)
    {
        if (Vector2.Distance(transform.position, target) < tolerance)
        {
            goingUp = !goingUp;
        }
    }
}
