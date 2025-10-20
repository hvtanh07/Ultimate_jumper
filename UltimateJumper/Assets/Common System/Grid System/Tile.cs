using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    [SerializeField] private bool isWalkable = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector2Int GridPosition => gridPosition;
    public bool IsWalkable => isWalkable;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetGridPosition(Vector2Int position)
    {
        gridPosition = position;
    }

    public void SetWalkable(bool walkable)
    {
        isWalkable = walkable;
    }

    public void SetColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }

    public void ResetColor()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    public void Highlight(Color highlightColor)
    {
        SetColor(highlightColor);
    }

    public void OnTileClicked()
    {
        // Implement custom logic for when the tile is clicked
    }

    public void OnTileHovered()
    {
        // Implement custom logic for when the tile is hovered
    }
}
