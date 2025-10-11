using UnityEngine;

public class Gazer : MonoBehaviour
{
    private Vector2 topPos = new Vector2(2f, 3f);
    private Vector2 bottomPos = new Vector2(2f, -3f);

    public float speed = 2f;

    private bool goingUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
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
        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            goingUp = !goingUp;
        }
   }
}
