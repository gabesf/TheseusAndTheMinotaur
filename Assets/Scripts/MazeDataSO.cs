using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public int[] theseusStartingPosition;
    public int[] minotaurStartingPosition = new int[2];
    public int[] gridSize = new int[2];
    public int[] mazeExit = new int[2];


    public bool reset = false;

    public List<GridUnitData> gridsUnitsWithWalls;

    private void OnValidate()
    {
        if(reset == true)
        {
            reset = false;
            gridsUnitsWithWalls = new List<GridUnitData>();
        }
    }

    private void OnEnable()
    {
        //gridsUnitsWithWalls = new List<GridUnitData>();
        if (gridsUnitsWithWalls == null)
        {
            Debug.Log("Reseting gridsWithWalls");
            gridsUnitsWithWalls = new List<GridUnitData>();
        }
    }
}
