using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject[] lightTrapPrefabs;
    public GameObject[] darkTrapPrefabs;
    public GameObject batteryPrefab;
    public GameObject exitPrefab;

    public float trapSpawnRate = 0.1f;

    private int[,] maze;
    private Vector2Int startPosition;
    private Vector2Int exitPosition;
    private List<Vector2Int> pathPositions = new List<Vector2Int>();

    private readonly Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };


    void Start()
    {
        GenerateMaze();
        RenderMaze();
        PlaceTraps();
        PlaceBatteries(4);
        PlaceExit();
    }

    void GenerateMaze()
    {
        startPosition = new Vector2Int(1, 1);
        exitPosition = new Vector2Int(width - 2, height - 2);
        maze = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = (x == 0 || x == width - 1 || y == 0 || y == height - 1) ? 0 : -1;
            }
        }

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int start = new Vector2Int(1, 1);
        maze[start.x, start.y] = 1;
        stack.Push(start);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                stack.Push(current);
                Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];
                Vector2Int between = (current + chosenNeighbor) / 2;

                maze[between.x, between.y] = 1;
                maze[chosenNeighbor.x, chosenNeighbor.y] = 1;
                stack.Push(chosenNeighbor);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == -1)
                {
                    maze[x, y] = 0;
                }
                else if (maze[x, y] == 1)
                {
                    pathPositions.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    void RenderMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                if (maze[x, y] == 0)
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (maze[x, y] == 1)
                {
                    Instantiate(pathPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    void PlaceTraps()
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (maze[x, y] == 1 && Random.value < trapSpawnRate && !HasAdjacentTrap(x, y))
                {
                    Vector3 position = new Vector3(x, y, 0);

                    bool isLightTrap = Random.value > 0.5f;
                    GameObject[] selectedTraps = isLightTrap ? lightTrapPrefabs : darkTrapPrefabs;

                    if (selectedTraps.Length > 0)
                    {
                        GameObject trapPrefab = selectedTraps[Random.Range(0, selectedTraps.Length)];
                        GameObject trap = Instantiate(trapPrefab, position, Quaternion.identity);

                        Trap trapScript = trap.GetComponent<Trap>();
                        if (trapScript != null)
                        {
                            trapScript.isLightTrap = isLightTrap;
                        }
                    }
                }
            }
        }
    }
    bool HasAdjacentTrap(int x, int y)
    {
        foreach (Vector2Int dir in directions)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;
            if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                continue;
            if (maze[nx, ny] == 3)
                return true;
        }
        return false;
    }


    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = cell + dir * 2;

            if (neighbor.x > 0 && neighbor.x < width - 1 && neighbor.y > 0 && neighbor.y < height - 1 && maze[neighbor.x, neighbor.y] == -1)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
    void PlaceBatteries(int count)
    {
        int placed = 0;
        int maxAttempts = 100; 
        while (placed < count && maxAttempts > 0)
        {
            maxAttempts--;
            Vector2Int randomPosition = pathPositions[Random.Range(0, pathPositions.Count)];
            if (maze[randomPosition.x, randomPosition.y] == 1)
            {
                Instantiate(batteryPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
                maze[randomPosition.x, randomPosition.y] = 2;
                placed++;
            }
        }
    }

    void PlaceExit()
    {
        if (maze[exitPosition.x, exitPosition.y] == 1)
        {
            Instantiate(exitPrefab, new Vector3(exitPosition.x, exitPosition.y, 0), Quaternion.identity);
        }
    }
}
