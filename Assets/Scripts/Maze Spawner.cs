using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public Vector3 CellSize = new Vector3(1, 1, 1); // Размеры одной ячейки

    private const float MazeWidth = 150.0f; // Фиксированная ширина лабиринта
    private const float MazeHeight = 150.0f; // Фиксированная высота лабиринта

    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        int width = maze.GetLength(0);
        int height = maze.GetLength(1);

        // Размеры ячеек для того, чтобы лабиринт соответствовал фиксированным размерам
        float cellWidth = MazeWidth / width;
        float cellHeight = MazeHeight / height;

        Vector3 mazeCenterOffset = new Vector3(
            (width * cellWidth) / 2.0f,
            0,
            (height * cellHeight) / 2.0f
        );

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellPosition = new Vector3(
                    x * cellWidth,
                    0,
                    y * cellHeight
                ) - mazeCenterOffset;

                GameObject cellObject = Instantiate(CellPrefab, cellPosition, Quaternion.identity);
                cellObject.transform.SetParent(transform, false);

                // Убедитесь, что значения CellSize.x и CellSize.z не равны нулю
                float scaleX = CellSize.x != 0 ? cellWidth / CellSize.x : 1;
                float scaleZ = CellSize.z != 0 ? cellHeight / CellSize.z : 1;

                // Масштабирование ячейки
                cellObject.transform.localScale = new Vector3(scaleX, 1, scaleZ);

                Cell c = cellObject.GetComponent<Cell>();
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
                if (c.Floor != null) c.Floor.SetActive(maze[x, y].Floor);
            }
        }
    }
}