using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Vector2 direction; 

    // Public method for the Skeleton to call when launching
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; 
        
        // Simple rotation to face the direction of travel
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle+45);
    }

    void Start()
    {
        // transform.rotation = Quaternion.Euler(0, 0, 45);
        // Destroy the arrow after a few seconds
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        // Move the arrow using its Transform
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}