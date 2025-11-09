using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Node rootNode;
    public LevelManager levelManager; // Provides day/night info
    public float velocity = 2f;
    public float direction;
    private float currentTime;

    private void Start()
    {
        levelManager = LevelManager.instance;
    }
    private void Update()
    {
        if (rootNode == null || levelManager == null) return;

        rootNode.Decide(gameObject, levelManager);
    }

    public void ExecuteAction(string action)
    {
        switch (action)
        {
            case "Wander":
                Wander();
                break;
            case "Flee":
                Flee();
                break;
            case "Chase":
                Chase();
                break;
        }
    }

    private void Wander()
    {
        currentTime += Time.deltaTime;
        Vector3 wanderDir = new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0);
        transform.position += wanderDir * velocity * Time.deltaTime;

        if (currentTime >= 2.0f)
        {
            direction = Random.Range(0.0f, Mathf.PI * 2f);
            currentTime = 0f;
        }
    }

    private void Flee()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 fleeDir = (transform.position - player.transform.position).normalized;
        transform.position += fleeDir * velocity * Time.deltaTime;
    }

    private void Chase()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.position += dir * velocity * Time.deltaTime;
    }
}
