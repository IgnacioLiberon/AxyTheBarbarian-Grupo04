using UnityEngine;

public class Gazer : MonoBehaviour
{
    private Vector2 topPos = new Vector2(2f, 3f);
    private Vector2 bottomPos = new Vector2(2f, -3f);

    public float speed = 2f;

    private bool goingUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = goingUp ? topPos : bottomPos;

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            goingUp = !goingUp;
        }
    }
}
