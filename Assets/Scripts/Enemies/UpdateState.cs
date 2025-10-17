using UnityEngine;

//This will be the first file to fall for the next assignment
//Let us do state machines please! Could've saved so many headaches!
public class UpdateState : MonoBehaviour
{
    [SerializeField] private Vector2 topPos = new Vector2(2f, 3f);
    [SerializeField] private Vector2 bottomPos = new Vector2(2f, -3f);

    public Vector2 target;
    private bool goingUp;

    private void Awake()
    {
        target = topPos;
        goingUp = true;
    }

    private void Update()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (Vector2.Distance(transform.position, target) < 0.05f)
            target = (goingUp = !goingUp) ? topPos : bottomPos;
    }
}
