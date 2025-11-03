using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways] // también se construye en Editor
public class GridGraph : MonoBehaviour
{
    [Header("Tilemap de muros (obstáculos)")]
    public Tilemap wallsTilemap;

    // Datos públicos útiles
    public int width  { get; private set; }
    public int height { get; private set; }
    public float cellSize { get; private set; }
    public Vector3 originWorld { get; private set; }

    // Internos
    private GraphNode[,] nodes;
    private BoundsInt bounds;
    private bool built = false;

    private void Awake()      => TryBuild();
    private void OnEnable()   => TryBuild();
    private void OnValidate() => TryBuild(); // reconstruye al cambiar en el Inspector

    private void TryBuild()
    {
        if (!isActiveAndEnabled) return;
        if (wallsTilemap == null) { built = false; return; }
        Build();
    }
    
    /// Construye la grilla: toda celda SIN tile en wallsTilemap es caminable.
    public void Build()
    {
        bounds = wallsTilemap.cellBounds;
        width  = Mathf.Max(0, bounds.size.x);
        height = Mathf.Max(0, bounds.size.y);

        if (width == 0 || height == 0)
        {
            nodes = null;
            built = false;
            return;
        }

        cellSize    = wallsTilemap.layoutGrid.cellSize.x; // se asume celda cuadrada
        originWorld = wallsTilemap.CellToWorld(bounds.min);

        nodes = new GraphNode[width, height];

        for (int dx = 0; dx < width; dx++)
        {
            for (int dy = 0; dy < height; dy++)
            {
                var cell = new Vector3Int(bounds.xMin + dx, bounds.yMin + dy, 0);
                bool isWall   = wallsTilemap.HasTile(cell);
                bool walkable = !isWall;
                Vector3 worldPos = wallsTilemap.GetCellCenterWorld(cell);
                nodes[dx, dy] = new GraphNode(dx, dy, worldPos, walkable);
            }
        }

        built = true;
    }

    public GraphNode GetNode(int x, int y)
    {
        if (nodes == null) return null;
        if (x < 0 || y < 0 || x >= width || y >= height) return null;
        return nodes[x, y];
    }

    public GraphNode GetNodeFromWorld(Vector3 worldPos)
    {
        if (nodes == null) return null;
        Vector3Int cell = wallsTilemap.WorldToCell(worldPos);
        int dx = cell.x - bounds.xMin;
        int dy = cell.y - bounds.yMin;
        return GetNode(dx, dy);
    }

    /// Vecinos en 4 direcciones (arriba, abajo, izquierda, derecha).
    public IEnumerable<GraphNode> GetNeighbors(GraphNode node)
    {
        if (nodes == null || node == null) yield break;

        // 4-neighborhood
        int[,] d4 = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        for (int i = 0; i < d4.GetLength(0); i++)
        {
            int nx = node.X + d4[i, 0];
            int ny = node.Y + d4[i, 1];
            var n = GetNode(nx, ny);
            if (n != null && n.Walkable)
                yield return n;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!built || nodes == null) return;

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            var n = nodes[x, y];
            Gizmos.color = n.Walkable ? new Color(0f, 1f, 0f, 0.12f) : new Color(1f, 0f, 0f, 0.35f);
            Gizmos.DrawCube(n.WorldPos, Vector3.one * (cellSize * 0.92f));
        }
    }
}
