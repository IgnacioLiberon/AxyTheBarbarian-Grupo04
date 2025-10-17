using UnityEngine;

public class Enemy : Entity
{
    protected override void Update()
    {
        base.Update();
        UpdateState();
    }

    protected virtual void UpdateState()
    {
        
    }
}
