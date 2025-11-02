using UnityEngine;

public class Rat : MonoBehaviour
{
    float direction = 0.0f;
    float velocity = 2.0f;
    float timeToChange = 2.0f;
    float currentTime = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var gamePlayer = GameObject.FindWithTag("Player");
        if (gamePlayer != null)
        {
            var playerPosition = gamePlayer.transform.position;
            var ratPosition = transform.position;
            var distance = Vector3.Distance(playerPosition, ratPosition);

            if (distance < 5.0f)
            {
                flee(gamePlayer);
            }
            else
            {
                Wander();
            }
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        currentTime += Time.deltaTime;
        var wanderDirection = new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0);
        transform.position += wanderDirection * velocity * Time.deltaTime;
        if (currentTime < timeToChange)
        {
            return;
        }
        else
        {
            currentTime = 0.0f;
        }
        direction += Random.Range(0.0f, 1.0f) * Mathf.PI * 2.0f;
    }
    
    void flee(GameObject player)
    {
        currentTime = 0.0f;
        var playerPosition = player.transform.position;
        var ratPosition = transform.position;
        var fleeDirection = (ratPosition - playerPosition).normalized;
        transform.position += fleeDirection * velocity * Time.deltaTime;
    }
}
