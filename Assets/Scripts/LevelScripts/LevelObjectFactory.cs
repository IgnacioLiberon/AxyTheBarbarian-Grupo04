using UnityEngine;
using UnityEngine.Tilemaps;

public enum LevelObjectType
{
    Wall,
    Exit,
    Gazer,
    Skeleton
}

public class LevelObjectFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject exitPrefab;
    public GameObject gazerPrefab;
    public GameObject skeletonPrefab;

    [Header("Tilemap")]
    public Tilemap wallTilemap;
    public TileBase wallTile;

    public void Create(LevelObjectType type, Vector3 position)
    {
        switch (type)
        {
            case LevelObjectType.Wall:
                if (wallTilemap != null && wallTile != null)
                    wallTilemap.SetTile(Vector3Int.FloorToInt(position), wallTile);
                break;

            case LevelObjectType.Exit:
                if (exitPrefab != null)
                    Instantiate(exitPrefab, position, Quaternion.identity);
                break;

            case LevelObjectType.Gazer:
                if (gazerPrefab != null)
                    Instantiate(gazerPrefab, position, Quaternion.identity);
                break;

            case LevelObjectType.Skeleton:
                if (skeletonPrefab != null)
                    Instantiate(skeletonPrefab, position, Quaternion.identity);
                break;
        }
    }
}
