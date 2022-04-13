using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MazeEditor : MonoBehaviour
{
    private int[] gridHovered;
    private RelativePosition relativePositionHovered;
    private MazeDataSO currentMazeData;
    public GameManager gameManager;
    private void Start()
    {
        EditingActions.OnGridUnitHovered += HandleOnGridHovered;
        EditingActions.OnWallHovered += HandleOnWallHovered;
        EditingActions.OnWallNotHovered += HandleOnWallNotHovered;
        EditingActions.OnWallClicked += HandleOnWallClicked;
        InputManagerActions.OnEditState += HandleOnEditState;
        //InputManagerActions.OnEditState += HandleOnEditState;
    }

    private void OnDisable()
    {
        EditingActions.OnGridUnitHovered -= HandleOnGridHovered;
        EditingActions.OnWallHovered -= HandleOnWallHovered;
    }

    private void HandleOnEditState(bool value)
    {
        if(value == true)
        {
            currentMazeData = gameManager.GetCurrentMazeData();
            Debug.Log($"The current Maze is a {currentMazeData.gridSize[0]}");
        }
    }

    private void HandleOnWallClicked(WallStatus wallStatus)
    {         
        if(gridHovered != null)
        {
            Debug.Log($"The {relativePositionHovered} wall of grid x: {gridHovered[0]} y: {gridHovered[1]} Status was set to {wallStatus}", gameObject);
            if (GameManager.gamestate == GameManager.GameState.EditingMaze)
            {
                int gridWithWalls = -1;
                for (int i = 0; i < currentMazeData.gridsUnitsWithWalls.Count; i++)
                {
                    if (currentMazeData.gridsUnitsWithWalls[i].gridPosition[0] == gridHovered[0] && currentMazeData.gridsUnitsWithWalls[i].gridPosition[1] == gridHovered[1])
                    {
                        gridWithWalls = i;
                    }

                }


                if (gridWithWalls != -1)
                {
                    if (wallStatus == WallStatus.OnMaze)
                    {
                        Debug.Log($"The grid unit that needs wall is {gridHovered[0]}:{gridHovered[1]}");
                        AddWall(currentMazeData.gridsUnitsWithWalls[gridWithWalls], relativePositionHovered);
                        //must add wall 
                    }
                    else
                    {
                        currentMazeData.gridsUnitsWithWalls[gridWithWalls] = RemoveWall(currentMazeData.gridsUnitsWithWalls[gridWithWalls], relativePositionHovered);
                        //must remove wall
                    }

                }
                else
                {
                    int[] position = { gridHovered[0], gridHovered[1] };
                    List<RelativePosition> relativeWallPositionList = new List<RelativePosition>();
                    relativeWallPositionList.Add(relativePositionHovered);
                    GridUnitData gridUnit = new GridUnitData(position, relativeWallPositionList);
                    currentMazeData.gridsUnitsWithWalls.Add(gridUnit);
                    Debug.Log("There are no walls on the grid unit");
                }

            }
        }

        
        //Debug.Log($"The current grid {currentMazeData} of maze data ")

    }

    private GridUnitData AddWall(GridUnitData gridUnitData, RelativePosition relativePositionHovered)
    {
        gridUnitData.walls.Add(relativePositionHovered);
        return gridUnitData;
        //Debug.Log($"The grid at {gridUnitData.gridPosition[0]}:{gridUnitData.gridPosition[1]} has walls. Must add " + relativePositionHovered);

    }

    private GridUnitData RemoveWall(GridUnitData gridUnitData, RelativePosition relativePositionHovered)
    {
        Debug.Log($"The grid at {gridUnitData.gridPosition[0]}:{gridUnitData.gridPosition[1]} has {gridUnitData.walls.Count} walls. Must check if must remove " + relativePositionHovered);
        if (gridUnitData.walls.Contains(relativePositionHovered))
        {
            gridUnitData.walls.Remove(relativePositionHovered);
            Debug.Log("Removed");
        } else
        {
            Debug.Log("Not Removed because there wasnt any");
        }

        Debug.Log($"The grid at {gridUnitData.gridPosition[0]}:{gridUnitData.gridPosition[1]} has {gridUnitData.walls.Count} walls");

        return gridUnitData;
    }

    private void HandleOnWallNotHovered()
    {
        //gridHovered = null;
    }

    private void HandleOnWallHovered(RelativePosition relativePosition)
    {
        if(GameManager.gamestate == GameManager.GameState.EditingMaze && gridHovered != null)
        {
            relativePositionHovered = relativePosition;
            //Debug.Log($"Hovering {relativePosition} Wall Of X:{gridHovered[0]} Y:{gridHovered[1]}");
        }      
    }
    private void HandleOnGridHovered(int[] position)
    {
        gridHovered = position;
        //Debug.Log($"Hovering {gridHovered[0]} {gridHovered[1]}");
    }



}
