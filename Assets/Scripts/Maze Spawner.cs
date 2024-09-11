using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject PlayerPrefab;
    public Vector3 CellSize = new Vector3(1, 1, 1);

    private const float MazeWidth = 150.0f;
    private const float MazeLength = 150.0f;

    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        int width = maze.GetLength(0);
        int length = maze.GetLength(1);

        float cellWidth = MazeWidth / width;
        float cellLength = MazeLength / length;

        Vector3 mazeCenterOffset = new Vector3((width * cellWidth) / 2.0f, 0, (length * cellLength) / 2.0f);

        // Создание и позиционирование клеток
        CreateAndPositionObjects(CellPrefab, (x, y) => new Vector3(x * cellWidth, 0, y * cellLength) - mazeCenterOffset, maze, width, length, cellWidth, cellLength);

        // Создание и позиционирование игрока
        CreateAndPositionObject(PlayerPrefab, new Vector3(0, 0, 0) - mazeCenterOffset, cellWidth, cellLength, true);
    }

    private void CreateAndPositionObjects(GameObject prefab, System.Func<int, int, Vector3> positionFunc, MazeGeneratorCell[,] maze, int width, int length, float cellWidth, float cellLength)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3 position = positionFunc(x, y);

                GameObject obj = Instantiate(prefab, position, Quaternion.identity);
                obj.transform.SetParent(transform, false);

                ScaleObject(obj, CellSize, cellWidth, cellLength, false);

                // Настроить специфические для клеток свойства
                if (prefab == CellPrefab)
                {
                    Cell c = obj.GetComponent<Cell>();
                    c.WallLeft.SetActive(maze[x, y].WallLeft);
                    c.WallBottom.SetActive(maze[x, y].WallBottom);
                    if (c.Floor != null) c.Floor.SetActive(maze[x, y].Floor);
                }
            }
        }
    }

    private void CreateAndPositionObject(GameObject prefab, Vector3 position, float cellWidth, float cellLength, bool scaleUniformly)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        ScaleObject(obj, CellSize, cellWidth, cellLength, scaleUniformly);
    }

    private void ScaleObject(GameObject obj, Vector3 cellSize, float cellWidth, float cellLength, bool scaleUniformly)
    {
        Vector3 originalScale = obj.transform.localScale;
        float scaleX = cellSize.x != 0 ? cellWidth / cellSize.x : 1;
        float scaleZ = cellSize.z != 0 ? cellLength / cellSize.z : 1;

        if (scaleUniformly)
        {
            // Масштабируем по всем осям, используя минимальный коэффициент из X и Z
            float scaleUniform = Mathf.Min(scaleX, scaleZ);
            obj.transform.localScale = new Vector3(originalScale.x * scaleUniform, originalScale.y * scaleUniform, originalScale.z * scaleUniform);
        }
        else
        {
            // Масштабируем по оси Y, используя меньший из коэффициентов X и Z
            float scaleY = Mathf.Min(scaleX, scaleZ);
            obj.transform.localScale = new Vector3(originalScale.x * scaleX, originalScale.y * scaleY, originalScale.z * scaleZ);
        }
    }
}