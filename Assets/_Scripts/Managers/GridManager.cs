using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _playableTile, _nonPlayableTile;
    [SerializeField] private Transform _camera;
    private Dictionary<Vector2, Tile> _pathTiles;

    void Awake() {
        Instance = this; 
    }

    public void GenerateGrid() {
        _pathTiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var isEdge = x == 0 || x == _width - 1 || y == 0 || y == _height - 1;
                var correctTile = isEdge ? _playableTile : _nonPlayableTile; 
                var tile = Instantiate(correctTile, new Vector3(x, y), Quaternion.identity);
                tile.name = $"Tile ({x}, {y})";
                tile.PathIndex = isEdge ? 0 : -1;

                _pathTiles[new Vector2(x, y)] = tile;
            }
        }

        // loop around edge of grid and set path index to 0
        var pathIndex = 0;
        var leftEdge = _pathTiles.Where(t => t.Key.x == 0).ToList();
        for (int i = 0; i < leftEdge.Count; i++) {
            leftEdge[i].Value.PathIndex = pathIndex;
            pathIndex++;
        }
        var topEdge = _pathTiles.Where(t => t.Key.y == _height - 1).ToList();
        for (int i = 1; i < topEdge.Count - 1; i++) {
            topEdge[i].Value.PathIndex = pathIndex;
            pathIndex++;
        }
        var rightEdge = _pathTiles.Where(t => t.Key.x == _width - 1).ToList();
        for (int i = rightEdge.Count - 1; i > 0; i--) {
            rightEdge[i].Value.PathIndex = pathIndex;
            pathIndex++;
        }
        var bottomEdge = _pathTiles.Where(t => t.Key.y == 0).ToList();
        for (int i = bottomEdge.Count - 1; i > 0; i--) {
            bottomEdge[i].Value.PathIndex = pathIndex;
            pathIndex++;
        }

        // set camera position to center of grid, -10 is "standard position" for camera?
        _camera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnUnits);
    }

    public Tile GetSpawnTile() {
        return _pathTiles.Where(t => t.Key.x == 0 && t.Key.y == 0).FirstOrDefault().Value;
    }
}
