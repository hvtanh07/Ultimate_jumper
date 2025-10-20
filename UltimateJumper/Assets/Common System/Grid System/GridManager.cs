using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _with, _height;
    [SerializeField] private Tile tilePrefab;
    private Tile[,] _tiles;

    public float _spacingVertical, _spacingHorizontal;

    void GenerateGrid()
    {
        _tiles = new Tile[_with, _height];
        for (int x = 0; x < _with; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 position = new Vector3(
                    x * (_spacingHorizontal + tilePrefab.transform.localScale.x),
                    y * (_spacingVertical + tilePrefab.transform.localScale.y),
                    0);
                var spawnedTile = Instantiate(tilePrefab, position, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                _tiles[x, y] = spawnedTile;
            }
        }
    }

    void GenerateGrid(int with,int height)
    {
        _with = with;
        _height = height;
        GenerateGrid();
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        if (x < 0 || x >= _with || y < 0 || y >= _height)
            return null;
        return _tiles[x, y];
    }


    public void HighlightTile(int x, int y, Color color)
    {
        var tile = GetTileAtPosition(x, y);
        if (tile != null)
        {
            //tile.Highlight(color);
        }
    }

    public void ResetGrid()
    {
        for (int x = 0; x < _with; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tile = _tiles[x, y];
                if (tile != null)
                {
                    //tile.ResetTile();
                }
            }
        }
    }
}
