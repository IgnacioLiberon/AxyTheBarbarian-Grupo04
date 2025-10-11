using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject arrowPrefab; 
    
    // Set the point on the skeleton where the arrow appears
    public Transform launchPoint; 
    
    public float throwCooldown = 2f; 
    private float stateTimer; //Time it remains during a state

    private void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateState();
    }

    private void UpdateState()
    {
        //When the shooting state timer runs out, it exits and re-enters shooting state. Could add a reload state
        if (stateTimer <= 0)
        {
            ShootArrows();
            stateTimer = throwCooldown;
        }
    }

    private void ShootArrows()
    {
        ThrowArrow();
        ThrowArrow();
        ThrowArrow();
    }

    private void ThrowArrow()
    {
        // 1. Create the arrow at the launch point
        GameObject arrowObject = Instantiate(arrowPrefab, launchPoint.position, launchPoint.rotation);
        
        // 2. Get the Arrow script
        Arrow arrowScript = arrowObject.GetComponent<Arrow>();
        
        // 3. Tell the arrow what direction to fly
        if (arrowScript != null)
        {
            Vector2 throwDirection = new Vector2(Random.Range(0f, 1f), Random.Range(-1f, 0f)); 
            arrowScript.SetDirection(throwDirection);
        }
    }
}