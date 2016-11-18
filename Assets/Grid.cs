using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    private const float TileSize = 0.64f;
    private const int GridSize = 32;
    private const float ZCoord = -10.0f;
    private TileState[,] grid = new TileState[GridSize, GridSize];

    public enum TileState { Empty, Obstructed };

	// Use this for initialization
	void Start () {
        InitializeGrid();
	}
	
	// Update is called once per frame
	void Update () {
        DrawGrid();
	}

    private void InitializeGrid() {
        for (int i = 0; i < GridSize; i++) {
            for (int j = 0; j < GridSize; j++) {
                if (TileObstructed(i, j)) {
                    grid[i, j] = TileState.Obstructed;
                }
            }
        }
    }

    private bool TileObstructed(int i, int j) {
        float x = IndexToCoord(i);
        float y = IndexToCoord(j);
        float margin = .01f;
        Vector2 bottomLeft = new Vector2(x + margin, y + margin);
        Vector2 bottomRight = new Vector2(x + TileSize - margin, y + margin);
        Vector2 topLeft = new Vector2(x + margin, y + TileSize - margin);
        Vector2 topRight = new Vector2(x + TileSize - margin, y + TileSize - margin);

        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in colliders) {
            if (collider.tag != "Wall")
                continue;
            if (collider.bounds.Contains(bottomLeft))
                return true;
            if (collider.bounds.Contains(bottomRight))
                return true;
            if (collider.bounds.Contains(topLeft))
                return true;
            if (collider.bounds.Contains(topRight))
                return true;
        }
        return false;
    }

    private float RoundToNearestMultiple(float numberToRound, float multipleOf) {
        int multiple = Mathf.RoundToInt(numberToRound / multipleOf);
        float ret = multiple * multipleOf;
        if (Mathf.Abs(ret) < 0.01f)
            ret = 0;
        return ret;
    }

    private void DrawGrid() {
        float gridStart = IndexToCoord(0);
        float gridEnd = IndexToCoord(GridSize-1);
        Color emptyColor = Color.white;
        for (int i = 0; i < GridSize; i++) {
            float coord = IndexToCoord(i);
            Debug.DrawLine(new Vector3(coord, gridStart, ZCoord), new Vector3(coord, gridEnd, ZCoord), emptyColor);
            Debug.DrawLine(new Vector3(gridStart, coord, ZCoord), new Vector3(gridEnd, coord, ZCoord), emptyColor);
        }

        Color obstructedColor = Color.red;
        for (int i = 0; i < GridSize; i++) {
            for (int j = 0; j < GridSize; j++) {
                if (grid[i, j] == TileState.Obstructed) {
                    float x = IndexToCoord(i);
                    float y = IndexToCoord(j);
                    Vector3 bottomLeft = new Vector3(x, y, ZCoord);
                    Vector3 bottomRight = new Vector3(x + TileSize, y, ZCoord);
                    Vector3 topLeft = new Vector3(x, y + TileSize, ZCoord);
                    Vector3 topRight = new Vector3(x + TileSize, y + TileSize, ZCoord);
                    Debug.DrawLine(bottomLeft, bottomRight, obstructedColor);
                    Debug.DrawLine(bottomRight, topRight, obstructedColor);
                    Debug.DrawLine(topRight, topLeft, obstructedColor);
                    Debug.DrawLine(topLeft, bottomLeft, obstructedColor);
                }
            }
        }
    }

    private float IndexToCoord(int i) {
        float halfTile = TileSize / 2;
        return halfTile + i * TileSize;
    }
}
