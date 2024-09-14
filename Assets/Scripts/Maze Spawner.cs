using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject PlayerPrefab;
    public Vector3 CellSize = new Vector3(1, 1, 1);

    private const float MazeWidth = 150.0f;
    private const float MazeLength = 150.0f;

    private IObjectSpawner objectSpawner;
    private IObjectScaler objectScaler;
    private IMazeGenerator mazeGenerator;

    void Start()
    {
        objectSpawner = new ObjectSpawner();
        objectScaler = new ObjectScaler();
        mazeGenerator = new DepthFirstMazeGenerator(); // Используем конкретную реализацию генератора

        MazeGeneratorCell[,] maze = mazeGenerator.GenerateMaze();

        int width = maze.GetLength(0);
        int length = maze.GetLength(1);

        float cellWidth = MazeWidth / width;
        float cellLength = MazeLength / length;

        Vector3 mazeCenterOffset = new Vector3((width * cellWidth) / 2.0f, 0, (length * cellLength) / 2.0f);

        // Создание клеток
        CreateAndPositionCells(width, length, maze, cellWidth, cellLength, mazeCenterOffset);

        // Создание игрока
        CreateAndPositionPlayer(mazeCenterOffset, cellWidth, cellLength);
    }

    private void CreateAndPositionCells(int width, int length, MazeGeneratorCell[,] maze, float cellWidth, float cellLength, Vector3 mazeCenterOffset)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3 position = new Vector3(x * cellWidth, 0, y * cellLength) - mazeCenterOffset;
                GameObject cell = objectSpawner.SpawnObject(CellPrefab, position, Quaternion.identity);
                cell.transform.SetParent(transform, false);
                objectScaler.ScaleObject(cell, CellSize, cellWidth, cellLength, false);

                Cell c = cell.GetComponent<Cell>();
                if (c != null)
                {
                    c.WallLeft.SetActive(maze[x, y].WallLeft);
                    c.WallBottom.SetActive(maze[x, y].WallBottom);
                    if (c.Floor != null)
                        c.Floor.SetActive(maze[x, y].Floor);
                }
            }
        }
    }

    private void CreateAndPositionPlayer(Vector3 mazeCenterOffset, float cellWidth, float cellLength)
    {
        Vector3 position = new Vector3(0, 0, 0) - mazeCenterOffset;
        GameObject player = objectSpawner.SpawnObject(PlayerPrefab, position, Quaternion.identity);
        objectScaler.ScaleObject(player, CellSize, cellWidth, cellLength, true);
    }
}
