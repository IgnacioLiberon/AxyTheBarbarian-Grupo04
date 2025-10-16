using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public void Move(Vector2 direction, float deltaTime)
    {
        if (direction.sqrMagnitude > 0.001f)
        {
            Vector2 movement = direction.normalized * moveSpeed * deltaTime;
            transform.Translate(movement);
        }
    }
}
