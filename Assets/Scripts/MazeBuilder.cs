using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
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

    private void Awake()
    {
        
        maze = new GameObject("Maze");
        maze.transform.position = new Vector3(-12.35f, -7.35f, 0f);
    }

    public void BuildMaze(MazeDataSO mazeData)
    {
        //DestroyMaze(mazeData.gridSize);
        DeactivateUnusedMaze(mazeData.gridSize);
        mazeGrid = new GridUnitHandler[mazeData.gridSize[0], mazeData.gridSize[1]];
        BuildGrid(mazeData.gridSize);
        SetBorderWalls(mazeData.gridSize);

 

        //mazeData.gridsUnitsWithWalls.Add(new GridUnitData(gridPosition, wallsRelativePosition));

        SetWalls(mazeData.gridsUnitsWithWalls);
        SetMazeExit(mazeData.mazeExit);
        SetEntitiesPosition(mazeData.theseusStartingPosition, mazeData.minotaurStartingPosition);
    }

    private void SetEntitiesPosition(int[] theseusStartingPosition, int[] minotaurStartingPosition)
    {
        
        GameActions.OnMazeIsBuilt?.Invoke(mazeGrid);
        GameActions.OnTheseusMoveRequest?.Invoke(theseusStartingPosition);
        GameActions.OnMinotaurMoveRequest?.Invoke(minotaurStartingPosition);
    }

    private void DestroyMaze(int[] gridSize)
    {
        if(mazeGrid != null)
        {
            foreach (var gridUnit in mazeGrid)
            {
                Destroy(gridUnit.gameObject);
            }
        }
    }
    private void DeactivateUnusedMaze(int[] gridSize)
    {
        
        if (mazeGrid != null)
        {
            Debug.Log("Will deactivate the maze");
            foreach (var gridUnit in mazeGrid)
            {
                gridUnit.gameObject.SetActive(false);
                //gridUnit.gameObject.SetActive(true);
                int[] positionCoordinates = gridUnit.GetPositionCoordinates();
                if (positionCoordinates[0] > gridSize[0] || positionCoordinates[1] > gridSize[1])
                {
                    Debug.Log(gridUnit.transform.name);
                    
                }
            }        
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
        Debug.Log("Building Grid " + gridUnitPrefab.name);
        Debug.Log($"Grid size {gridSize[0]}");
        for (int i = 0; i < gridSize[0]; i++)
        {
            for (int k = 0; k < gridSize[1]; k++)
            {
                GameObject gridUnit = Instantiate(gridUnitPrefab);

                gridUnit.transform.parent = maze.transform;
                gridUnit.transform.localPosition = new Vector3(i * 1.05f, k * 1.05f);
                gridUnit.transform.name = $"({i}, {k})";               
                mazeGrid[i, k] = gridUnit.GetComponent<GridUnitHandler>();
                
                int[] position = { i, k };
                mazeGrid[i, k].SetPositionCoordinates(position);
            }
        }
    }

   
}
