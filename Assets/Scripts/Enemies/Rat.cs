using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Rat : MonoBehaviour
{
    private EnemyAI ai;

    private void Awake()
    {
        ai = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        if (ai == null)
        {
            Debug.LogError("Missing EnemyAI script");
            return;
        }

        ai.rootNode = CreateDefaultDecisionTree();
    }

    private Node CreateDefaultDecisionTree()
    {
        // --- Evaluators ---
        var distanceEval = ScriptableObject.CreateInstance<DistanceDecisionEvaluator>();
        distanceEval.threshold = 5f;

        var daytimeEval = ScriptableObject.CreateInstance<DaytimeEvaluator>();

        // --- Decisions ---
        var nearPlayerDecision = ScriptableObject.CreateInstance<ObjectDecision>();
        nearPlayerDecision.evaluator = distanceEval;

        var isDaytimeDecision = ScriptableObject.CreateInstance<ObjectDecision>();
        isDaytimeDecision.evaluator = daytimeEval;

        // --- Actions ---
        var wanderAction = ScriptableObject.CreateInstance<ActionNode>();
        wanderAction.actionName = "Wander";

        var fleeAction = ScriptableObject.CreateInstance<ActionNode>();
        fleeAction.actionName = "Flee";

        var chaseAction = ScriptableObject.CreateInstance<ActionNode>();
        chaseAction.actionName = "Chase";

        // --- Binary Nodes ---
        var dayNightBranch = ScriptableObject.CreateInstance<BinaryDecisionNode>();
        dayNightBranch.decision = isDaytimeDecision;
        dayNightBranch.yesNode = fleeAction;  // if daytime
        dayNightBranch.noNode = chaseAction;  // if nighttime

        var distanceBranch = ScriptableObject.CreateInstance<BinaryDecisionNode>();
        distanceBranch.decision = nearPlayerDecision;
        distanceBranch.yesNode = dayNightBranch;
        distanceBranch.noNode = wanderAction;

        // Root node of the rat AI
        return distanceBranch;
    }
}
