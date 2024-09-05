using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeGeneratorCell
{
    public int X;
    public int Y;
    public bool WallLeft = true;
    public bool WallBottom = true;
}
public class MazeGenerator
{
    public int Widht = 25;
    public int Height = 15;
    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Widht, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }

            
        }
        return maze;
    }
}
