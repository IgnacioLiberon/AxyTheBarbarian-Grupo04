using System.Linq;
using UnityEngine;

public class LevelEditorRuntime : MonoBehaviour
{
    [Header("References")]
    public LevelManager levelManager;
    public Camera sceneCamera;

    [Header("Placement Settings")]
    public LevelObjectType currentType = LevelObjectType.Wall;
    public string currentSubtype = "North";
    public bool editingEnabled = true; // toggleable at runtime

    private GridWallPlacementHandler wallHandler;

    private void Awake()
    {
        if (sceneCamera == null)
            sceneCamera = Camera.main;

        // Try to find wall handler in the factory
        if (levelManager != null && levelManager.factory != null)
            wallHandler = levelManager.factory.GetComponentInChildren<GridWallPlacementHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            editingEnabled = !editingEnabled;
            Debug.Log("Editing mode: " + editingEnabled);
        }

        if (!editingEnabled) return;

        HandleClick();
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to place
        {
            Vector3 mousePos = Input.mousePosition;
            TryModifyAtScreenPosition(mousePos, place: true);
        }
        else if (Input.GetMouseButtonDown(1)) // Right click to delete
        {
            Vector3 mousePos = Input.mousePosition;
            TryModifyAtScreenPosition(mousePos, place: false);
        }
    }

    private void TryModifyAtScreenPosition(Vector3 screenPos, bool place)
    {
        if (sceneCamera == null) return;

        Ray ray = sceneCamera.ScreenPointToRay(screenPos);
        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            Vector3Int gridPos = Vector3Int.RoundToInt(worldPos);

            if (place)
                PlaceAndSave(gridPos);
            else
                DeleteAndSave(gridPos);
        }
    }

    private void PlaceAndSave(Vector3Int gridPos)
    {
        // Instantiate in scene
        levelManager.factory.Create(currentType, gridPos, currentSubtype);

        // Update JSON data
        LevelData levelData = levelManager.GetLoadedLevel();
        if (levelData == null)
            levelData = new LevelData();

        // Avoid duplicate entries
        if (!levelData.objects.Any(o => o.x == gridPos.x && o.y == gridPos.y))
        {
            levelData.objects.Add(new LevelObjectData
            {
                type = currentType.ToString(),
                subtype = currentSubtype,
                x = gridPos.x,
                y = gridPos.y
            });

            levelManager.SaveLevel(levelData);
            Debug.Log($"[Editor] Placed {currentType} ({currentSubtype}) at {gridPos}");
        }
        else
        {
            Debug.Log($"[Editor] Skipped duplicate placement at {gridPos}");
        }
    }

    private void DeleteAndSave(Vector3Int gridPos)
    {
        LevelData levelData = levelManager.GetLoadedLevel();
        if (levelData == null || levelData.objects.Count == 0)
        {
            Debug.LogWarning("[Editor] No objects in level data to delete.");
            return;
        }

        // Find entry in JSON
        var target = levelData.objects.FirstOrDefault(o => o.x == gridPos.x && o.y == gridPos.y);
        if (target == null)
        {
            Debug.Log($"[Editor] No object found to delete at {gridPos}");
            return;
        }

        // Remove from tilemap or scene
        if (target.type == LevelObjectType.Wall.ToString())
        {
            if (wallHandler != null)
            {
                wallHandler.GetComponentInChildren<UnityEngine.Tilemaps.Tilemap>()?
                    .SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), null);
            }
        }
        else
        {
            // Try finding matching prefab instance (by grid position)
            Collider2D hit = Physics2D.OverlapPoint(new Vector2(gridPos.x, gridPos.y));
            if (hit != null)
            {
                Destroy(hit.gameObject);
            }
        }

        // Remove from JSON and save
        levelData.objects.Remove(target);
        levelManager.SaveLevel(levelData);
        Debug.Log($"[Editor] Deleted {target.type} at {gridPos}");
    }
}
