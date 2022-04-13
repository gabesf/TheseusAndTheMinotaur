using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridUnitData
{
    public List<RelativePosition> walls;
    public int[] gridPosition;

    public GridUnitData(int[] gridPosition, List<RelativePosition> walls) 
    {
        this.walls = walls;
        this.gridPosition = gridPosition;
    }
    
}

[CreateAssetMenu(menuName ="MazeData")]
public class MazeDataSO : ScriptableObject
{
    public int[] theseusStartingPosition = new int[2];
    public int[] minotaurStartingPosition = new int[2];
    public int[] gridSize = new int[2];
    public int[] mazeExit = new int[2];
    public List<GridUnitData> gridsWithWalls = new List<GridUnitData>();
}
