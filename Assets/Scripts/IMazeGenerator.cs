using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMazeGenerator
{
    MazeGeneratorCell[,] GenerateMaze();
}
public class MazeGeneratorCell
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool WallLeft { get; set; } = true;
    public bool WallBottom { get; set; } = true;
    public bool Floor { get; set; } = true;
    public bool Visited { get; set; } = false;
    public int DistanceFromStart { get; set; }
}
