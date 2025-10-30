using UnityEngine;

public interface IWallPlacementHandler
{
    void PlaceWall(Vector3Int gridPosition, WallVariant variant);
}
