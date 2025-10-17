using UnityEngine;

public class Skeleton : Entity
{
    private Timer timer;
    private Shoot shooter;
    
    protected override void Awake()
    {
        base.Awake();
        timer = GetComponent<Timer>();
        shooter = GetComponent<Shoot>();
    }

    protected override void Update()
    {
        base.Update();

        if (timer.hasTriggered)
        {
            shooter.ShootArrows();
            timer.SetTimer();
        }
    }
}