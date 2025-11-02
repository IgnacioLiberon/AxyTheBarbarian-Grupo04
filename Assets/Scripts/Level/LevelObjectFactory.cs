using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelObjectFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject exitPrefab;
    public GameObject gazerPrefab;
    public GameObject skeletonPrefab;
    public GameObject ratPrefab;

    public GameObject zombiePrefab;

    [Header("Wall Handler")]
    public MonoBehaviour wallHandlerComponent;
    private IWallPlacementHandler wallHandler;

    private void Awake()
    {
        if (wallHandlerComponent != null)
            wallHandler = wallHandlerComponent as IWallPlacementHandler;
        else
            Debug.LogWarning("No wall handler component assigned to LevelObjectFactory.");
    }

    public void Create(LevelObjectType type, Vector3 position, string subtype = null)
    {
        Vector3Int gridPosition = Vector3Int.FloorToInt(position);

        switch (type)
        {
            case LevelObjectType.Wall:
                if (wallHandler != null && Enum.TryParse(subtype, out WallVariant variant))
                    wallHandler.PlaceWall(gridPosition, variant);
                else
                    Debug.LogWarning($"Failed to place wall at {gridPosition} (missing handler or invalid subtype).");
                break;

            case LevelObjectType.Exit:
                if (exitPrefab != null)
                    Instantiate(exitPrefab, gridPosition, Quaternion.identity);
                break;

            case LevelObjectType.Gazer:
                if (gazerPrefab != null)
                    Instantiate(gazerPrefab, gridPosition, Quaternion.identity);
                break;

            case LevelObjectType.Skeleton:
                if (skeletonPrefab != null)
                    Instantiate(skeletonPrefab, gridPosition, Quaternion.identity);
                break;

            case LevelObjectType.Rat:
                if (ratPrefab != null)
                    Instantiate(ratPrefab, gridPosition, Quaternion.identity);
                break;
            
            case LevelObjectType.HungryZombie:
                if (zombiePrefab != null)
                    Instantiate(zombiePrefab, gridPosition, Quaternion.identity);
                break;
        }
    }
}
