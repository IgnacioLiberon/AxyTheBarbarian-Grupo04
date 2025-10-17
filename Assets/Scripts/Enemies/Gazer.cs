using UnityEngine;

public class Gazer : Enemy
{
    private Movement movement;
    private UpdateState state;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
        state = GetComponent<UpdateState>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement.MoveTo(state.target);
    }
}
