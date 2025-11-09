using UnityEngine;

[CreateAssetMenu(menuName = "AI/Evaluators/Distance To Player")]
public class DistanceDecisionEvaluator : ObjectEvaluator
{
    public float threshold = 5f;

    public override bool Function(GameObject obj, object world)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return false;

        float distance = Vector3.Distance(obj.transform.position, player.transform.position);
        return distance <= threshold;
    }
}
