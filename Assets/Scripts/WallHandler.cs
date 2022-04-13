using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallStatus
{
    OnMaze,
    NotOnMaze,
    HighLighted
}
public class WallHandler : MonoBehaviour
{
    //I will sleep now :) 
    private WallStatus wallStatus;
    private WallStatus previousWallStatus;
    public SpriteRenderer spriteRenderer;
    private void Awake()
    {
        SetIsOnMaze(false);
    }
    private void OnMouseOver()
    {
        Debug.Log("Mouse over " + GameManager.gamestate.ToString());
        if(GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            previousWallStatus = wallStatus;
            wallStatus = WallStatus.HighLighted;
        }

        UpdateWallRenderer();
        
    }

    private void OnMouseExit()
    {
        if (GameManager.gamestate == GameManager.GameState.EditingMaze)
        {
            if(wallStatus == WallStatus.HighLighted)
            {
                wallStatus = previousWallStatus;
            }
            
        }

        UpdateWallRenderer();
    }

    private void HighlightWall(bool value)
    {
        if(value == true)
        {   
            
            spriteRenderer.color = Color.blue;
        } else 
        {
            spriteRenderer.color = Color.black;
        }
        
    }

    // St
    // art is called before the first frame updat



    internal void SetIsOnMaze(bool value)
    {
        if(value == true)
        {
            wallStatus = WallStatus.OnMaze;
        } else
        {
            wallStatus = WallStatus.NotOnMaze;
        }

        UpdateWallRenderer();
    }

    private void UpdateWallRenderer()
    {
        switch (wallStatus)
        {
            case WallStatus.OnMaze:
                spriteRenderer.color = Color.black;
                break;
            case WallStatus.NotOnMaze:
                spriteRenderer.color = new Color(0, 0, 0, 0);
                break;
            case WallStatus.HighLighted:
                spriteRenderer.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
