using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform launchPoint;

    public void ShootArrows()
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
