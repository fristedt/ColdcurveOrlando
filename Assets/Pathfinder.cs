using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {
    private Vector3 destination;
    private Grid grid;
    private Node[,] graph;


    private List<Vector2> path;

    private void Start() {
        grid = FindObjectOfType<Grid>();

        SetDestination(new Vector2(13, 8));
    }

    private void Update() {
        for (int i = 1; i < path.Count; i++) {
            Debug.DrawLine(path[i - 1], path[i], Color.blue);
        }
    }

    public void SetDestination(Vector2 newDestination) {
        destination = newDestination;
        int startX = grid.CoordToIndex(transform.position.x);
        int startY = grid.CoordToIndex(transform.position.y);
        int endX = grid.CoordToIndex(newDestination.x);
        int endY = grid.CoordToIndex(newDestination.y);
        Node goal = GenerateBFSTree(startX, startY, endX, endY);
        path = BacktrackTree(goal);
    }

    private void InitializeGraph(int startX, int startY) {
        graph = new Node[Grid.GridSize, Grid.GridSize];
        for (int i = 0; i < Grid.GridSize; i++) {
            for (int j = 0; j < Grid.GridSize; j++) {
                graph[i, j] = new Node(i, j);
            }
        }
    }

    private Node GenerateBFSTree(int startX, int startY, int endX, int endY) {
        InitializeGraph(startX, startY);

        Queue<Node> nodesToSearch = new Queue<Node>();
        graph[startX, startY].distance = 0;
        nodesToSearch.Enqueue(graph[startX, startY]);
        while (nodesToSearch.Count > 0) {
            Node current = nodesToSearch.Dequeue();
            List<Node> neighbours = GetNeighboursOf(current);
            foreach (Node n in neighbours) {
                if (n.distance != -1)
                    continue;
                n.distance = current.distance + 1;
                n.parent = current;

                if (n.x == endX && n.y == endY)
                    return n;

                nodesToSearch.Enqueue(n);
            }
        }
        return null;
    }

    private List<Vector2> BacktrackTree(Node node) {
        List<Vector2> path = new List<Vector2>();
        while (node.parent != null) {
            path.Add(new Vector2(grid.IndexToCenterCoord(node.x), grid.IndexToCenterCoord(node.y)));
            node = node.parent;
        }
        path.Reverse();
        return path;
    }

    private List<Node> GetNeighboursOf(Node node) {
        List<Node> neighbours = new List<Node>();
        if (node.x > 0 && grid[node.x - 1, node.y] == Grid.TileState.Empty)
            neighbours.Add(graph[node.x - 1, node.y]);
        if (node.y > 0 && grid[node.x, node.y - 1] == Grid.TileState.Empty)
            neighbours.Add(graph[node.x, node.y - 1]);
        if (node.x < Grid.GridSize - 1 && grid[node.x + 1, node.y] == Grid.TileState.Empty)
            neighbours.Add(graph[node.x + 1, node.y]);
        if (node.y < Grid.GridSize - 1 && grid[node.x, node.y + 1] == Grid.TileState.Empty)
            neighbours.Add(graph[node.x, node.y + 1]);
        return neighbours;
    }

    private class Node {
        public int distance = -1;
        public Node parent = null;
        public int x;
        public int y;

        public Node(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
