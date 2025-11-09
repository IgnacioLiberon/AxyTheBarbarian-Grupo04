using UnityEngine;

[CreateAssetMenu(menuName = "AI/Nodes/Action Node")]
public class ActionNode : Node
{
    public string actionName;

    public override Node Decide(GameObject obj, object world)
    {
        // Execute logic dynamically
        if (obj.TryGetComponent(out EnemyAI ai))
        {
            ai.ExecuteAction(actionName);
        }
        return this;
    }
}