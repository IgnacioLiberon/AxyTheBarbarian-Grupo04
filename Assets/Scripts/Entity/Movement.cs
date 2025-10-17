using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public void Move(Vector2 direction)
    {
        float deltaTime = Time.deltaTime;
        if (direction.sqrMagnitude > 0.001f)
        {
            Vector2 movement = direction.normalized * moveSpeed * deltaTime;
            transform.Translate(movement);
        }
    }

    public void MoveTo(Vector2 target)
    {
        float deltaTime = Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * deltaTime);
    }
}
