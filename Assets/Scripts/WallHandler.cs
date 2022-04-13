using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallStatus
{
    OnMaze,
    NotOnMaze,
}
public class WallHandler : MonoBehaviour
{
    private WallStatus wallStatus;
    private SpriteRenderer spriteRenderer;
    public RelativePosition relativePosition;

    public Color OnMazeOnEdit;
    public Color NotOnMazeOnEdit;
    public Color OnMazeOnPlay;
    public Color NotOnMazeOnPlay;
    private void Awake()
    {
        //Debug.Log("Wall Handler Awake");
        SetIsOnMaze(false);
        InputManagerActions.OnEditState += HandleOnEditState;
        GameActions.OnMazeIsBuilt += HandleOnMazeIsBuilt;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateWallRenderer();
    }

    private void HandleOnMazeIsBuilt(GridUnitHandler[,] grid)
    {
        UpdateWallRenderer();
    }


    private void OnDisable()
    {
        InputManagerActions.OnEditState -= HandleOnEditState;
    }
    private void OnDestroy()
    {
        InputManagerActions.OnEditState -= HandleOnEditState;
    }

    private void HandleOnEditState(bool value)
    {
        if (value == true)
        {
            RenderAsEditable();
        }
        else
        {
            RenderAsPlayable();
        }
    }

    private void RenderAsPlayable()
    {
        if (transform != null)
        {
            if (wallStatus == WallStatus.OnMaze)
            {
                spriteRenderer.color = OnMazeOnPlay;
            }
            else
            {
                spriteRenderer.color = NotOnMazeOnPlay;
            }
        }
    }

    private void RenderAsEditable()
    {
        if (wallStatus == WallStatus.OnMaze)
        {
            spriteRenderer.color = OnMazeOnEdit;
        }
        else
        {
            spriteRenderer.color = NotOnMazeOnEdit;
        }
    }

    private void OnMouseOver()
    {
        //Debug.Log(transform.name + " " + relativePosition);

        if (GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            //Debug.Log("Hovering a " + relativePosition + "wall");
            EditingActions.OnWallHovered.Invoke(relativePosition);
            //previousWallStatus = wallStatus;
            //wallStatus = WallStatus.HighLighted;
        }
        //
        //UpdateWallRenderer();

    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on wall");
        if (GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            if (wallStatus == WallStatus.OnMaze)
            {
                wallStatus = WallStatus.NotOnMaze;
            }
            else
            {
                wallStatus = WallStatus.OnMaze;
            }
        }

        EditingActions.OnWallClicked.Invoke(wallStatus);


        UpdateWallRenderer();


    }

    private void OnMouseExit()
    {
        if (GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            EditingActions.OnWallNotHovered.Invoke();

        }

        //UpdateWallRenderer();
    }



    internal void SetIsOnMaze(bool value)
    {
        if (value == true)
        {
            wallStatus = WallStatus.OnMaze;
        }
        else
        {
            wallStatus = WallStatus.NotOnMaze;
        }
        //UpdateWallRenderer();
        //UpdateWallRenderer();
    }

    private void UpdateWallRenderer()
    {
        if (GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            RenderAsEditable();
        }
        else
        {
            RenderAsPlayable();
        }

    }

    private void Update()
    {
        //Debug.Log(relativePosition);
    }
}
