using UnityEngine;
using UnityEngine.Tilemaps;

public class GridWallPlacementHandler : MonoBehaviour, IWallPlacementHandler
{
    private Tilemap wallTilemap;

    [Header("Wall Variants (8 Tiles)")]
    public TileBase wallNorth;
    public TileBase wallSouth;
    public TileBase wallEast;
    public TileBase wallWest;
    public TileBase wallCornerNE;
    public TileBase wallCornerNW;
    public TileBase wallCornerSE;
    public TileBase wallCornerSW;

    private void Awake()
    {
        wallTilemap = GetComponentInChildren<Tilemap>();
    }

   public void PlaceWall(Vector3Int gridPos, WallVariant variant)
    {
        if (wallTilemap == null)
        {
            Debug.LogWarning("No Tilemap assigned to GridWallPlacementHandler.");
            return;
        }
        TileBase tile = GetTileForVariant(variant);
        wallTilemap.SetTile(gridPos, tile);
    }

    public void ClearTile(Vector3Int gridPos)
    {
        if (wallTilemap == null)
        {
            Debug.LogWarning("No Tilemap assigned to GridWallPlacementHandler.");
            return;
        }
        wallTilemap.SetTile(gridPos, null);
    }

    private TileBase GetTileForVariant(WallVariant variant)
    {
        return variant switch
        {
            WallVariant.North => wallNorth,
            WallVariant.South => wallSouth,
            WallVariant.East => wallEast,
            WallVariant.West => wallWest,
            WallVariant.CornerNE => wallCornerNE,
            WallVariant.CornerNW => wallCornerNW,
            WallVariant.CornerSE => wallCornerSE,
            WallVariant.CornerSW => wallCornerSW,
            _ => wallNorth
        };
    }
}
