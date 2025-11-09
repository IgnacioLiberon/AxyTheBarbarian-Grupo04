using UnityEngine;

[CreateAssetMenu(menuName = "AI/Evaluators/Daytime Check")]
public class DaytimeEvaluator : ObjectEvaluator
{
    public override bool Function(GameObject obj, object world)
    {
        if (world is LevelManager level)
        {
            return level.IsDaytime;
        }
        return true;
    }
}
