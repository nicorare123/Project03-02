using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject[] trapPrefabs;

    public float trapSpawnRate = 0.1f;

    private int[,] maze;

    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    // Start is called before the first frame update
    void Start()
    {
        GenerateMaze();
        SpawnMaze();
        PlaceTraps();
    }
    void GenerateMaze()
    {
        maze = new int[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                maze[x,y] = 0;
            }
        }
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int currentCell = new Vector2Int(Random.Range(1, width) * 2, Random.Range(1, height) * 2);
        maze[currentCell.x, currentCell.y] = 1;

        stack.Push(currentCell);

        while (stack.Count > 0)
        {
            currentCell = stack.Pop();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(currentCell);

            if (neighbors.Count > 0)
            {
                stack.Push(currentCell);

                neighbors.Shuffle();
                Vector2Int chosenNeighbor = neighbors[0];

                Vector2Int between = (currentCell + chosenNeighbor) / 2;
                maze[between.x, between.y] = 1;
                maze[chosenNeighbor.x, chosenNeighbor.y] = 1;

                stack.Push(chosenNeighbor);
            }
        }
    }

    void SpawnMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                if (maze[x, y] == 1)
                {
                    Instantiate(floorPrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    void PlaceTraps()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 1 && Random.value < trapSpawnRate)
                {
                    Vector3 position = new Vector3(x, 0, y);

                    GameObject randomTrapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
                    GameObject trap = Instantiate(randomTrapPrefab, position, Quaternion.identity);

                    Trap trapScript = trap.GetComponent<Trap>();
                    trapScript.isLightTrap = Random.value > 0.5f;
                }
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbor = cell + direction * 2;

            if (neighbor.x >= 0 && neighbor.x < width && neighbor.y >= 0 && neighbor.y < height && maze[neighbor.x, neighbor.y] == 0)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}

    public static class ListExtensions
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;    
            }
        }
    }
    // Update is called once per frame
