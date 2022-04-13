using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public int[] mazeExit = new int[2];

    public GameObject gridUnitPrefab;
    public GameObject gridTheseuVictoryPrefab;

    private GameObject maze;
    private GridUnitHandler[,] mazeGrid;
    public GridUnitHandler[,] MazeGrid { get 
        {
            return mazeGrid;
        } set 
        {
            mazeGrid = value;
        } 
    }

    private void Start()
    {
        maze = new GameObject("Maze");
        maze.transform.position = new Vector3(-4.725f, -4.725f, 0f);
    }

    public void BuildMaze(MazeDataSO mazeData)
    {
        DestroyMaze();
        mazeGrid = new GridUnitHandler[mazeData.gridSize[0], mazeData.gridSize[1]];
        BuildGrid(mazeData.gridSize);
        SetBorderWalls(mazeData.gridSize);

        //testing
        int[] gridPosition = { 1, 1 };
        List<RelativePosition> wallsRelativePosition = new List<RelativePosition>();
        wallsRelativePosition.Add(RelativePosition.Down);
        wallsRelativePosition.Add(RelativePosition.Up);
        wallsRelativePosition.Add(RelativePosition.Right);
        wallsRelativePosition.Add(RelativePosition.Left);


        mazeData.gridsWithWalls.Add(new GridUnitData(gridPosition, wallsRelativePosition));

        SetWalls(mazeData.gridsWithWalls);
        SetMazeExit(mazeData.mazeExit);
        SetEntitiesPosition(mazeData.theseusStartingPosition, mazeData.minotaurStartingPosition);
        //ResetWalls();
        //SetWallsThatExist(walls);
    }

    private void SetEntitiesPosition(int[] theseusStartingPosition, int[] minotaurStartingPosition)
    {
        Debug.Log(mazeGrid[0, 0].transform.name);
        EntitiesActions.OnMazeIsBuilt?.Invoke(mazeGrid);
        EntitiesActions.OnTheseusMoveRequest?.Invoke(theseusStartingPosition);
        EntitiesActions.OnMinotaurMoveRequest?.Invoke(minotaurStartingPosition);
    }

    private void DestroyMaze()
    {
        if (mazeGrid != null)
        {
            foreach (var gridUnit in mazeGrid)
            {
                Destroy(gridUnit.gameObject);
            }
            
        }
        else
        {
            Debug.Log("Maze Dont Exist");
        }

        
    }

    private void SetMazeExit(int[] mazeExit)
    {
        //Debug.Log($"Setting {mazeExit[0]} as exit ");
        mazeGrid[mazeExit[0], mazeExit[1]].SetIsExit();
    }

    private void SetWalls(List<GridUnitData> gridsWithWalls)
    {
        foreach (GridUnitData gridUnit in gridsWithWalls)
        {
            foreach (RelativePosition wall in gridUnit.walls)
            {
                mazeGrid[gridUnit.gridPosition[0], gridUnit.gridPosition[1]].SetWall(wall, true);
            }
        }


        //mazeGrid[2, 2].SetWall(RelativePosition.Down, true);
        //mazeGrid[2, 2].SetWall(RelativePosition.Up, true);
        //mazeGrid[2, 2].SetWall(RelativePosition.Left, true);
    }

    private void SetBorderWalls(int[] gridSize)
    {
        //mazeGrid[0, 0].SetWall("bottomWall", true);
        for (int i = 0; i < gridSize[0]; i++)
        {
            mazeGrid[i, 0].SetWall(RelativePosition.Down, true);
            mazeGrid[i, gridSize[1] - 1].SetWall(RelativePosition.Up, true);

        }

        for (int i = 0; i < gridSize[1]; i++)
        {
            mazeGrid[0, i].SetWall(RelativePosition.Left, true);
            mazeGrid[gridSize[0] - 1, i].SetWall(RelativePosition.Right, true);
        }
    }


    private void BuildGrid(int[] gridSize)
    {
        for (int i = 0; i < gridSize[0]; i++)
        {
            for (int k = 0; k < gridSize[1]; k++)
            {
                GameObject gridUnit = Instantiate(gridUnitPrefab);

                gridUnit.transform.parent = maze.transform;
                gridUnit.transform.localPosition = new Vector3(i * 1.05f, k * 1.05f);
                gridUnit.transform.name = $"({i}, {k})";               
                mazeGrid[i, k] = gridUnit.GetComponent<GridUnitHandler>();
                
            }
        }
    }

   
}
