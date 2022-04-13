using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RelativePosition
{
    Up,
    Down,
    Left,
    Right
}

public class GridUnitHandler : MonoBehaviour
{
    private Dictionary<RelativePosition, bool> walls = new Dictionary<RelativePosition, bool>();

    public WallHandler topWall;
    public WallHandler rightWall;
    public WallHandler leftWall;
    public WallHandler bottomWall;

    private SpriteRenderer spriteRenderer;
    private bool IsExit = false;
    private void Awake()
    {
        walls.Add(RelativePosition.Up, false);
        walls.Add(RelativePosition.Right, false);
        walls.Add(RelativePosition.Left, false);
        walls.Add(RelativePosition.Down, false);
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse Over me");   
    }
    public void SetWall(RelativePosition wallRelativePosition, bool value)
    {
        if (walls.ContainsKey(wallRelativePosition))
        {
            walls[wallRelativePosition] = value;
            SetActiveWallSprite(wallRelativePosition, value);

        } else
        {
            throw new Exception("Dictionary does not contain " + wallRelativePosition);

        }

    }

    internal void SetIsExit()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 1, 0);
        IsExit = true;
    }

    internal bool GetIsExit()
    {
        return IsExit;
    }

    internal bool GetOppositeWall(RelativePosition wallRelativePosition)
    {
        wallRelativePosition = InvertWallRelativePosition(wallRelativePosition);
        return GetWall(wallRelativePosition);
        
        
    }

    private RelativePosition InvertWallRelativePosition(RelativePosition wallRelativePosition)
    {
        switch (wallRelativePosition)
        {
            case RelativePosition.Up:
                return RelativePosition.Down;                
            case RelativePosition.Down:
                return RelativePosition.Up;
            case RelativePosition.Left:
                return RelativePosition.Right;
            case RelativePosition.Right:
                return RelativePosition.Left;
            default:
                throw new Exception("Unexpected wall relative position " + wallRelativePosition.ToString());
        }
    }

    private void SetActiveWallSprite(RelativePosition wallRelativePosition, bool value)
    {
       
        switch (wallRelativePosition)
        {
            case RelativePosition.Up:
                topWall.SetIsOnMaze(value);
                break;

            case RelativePosition.Left:
                leftWall.SetIsOnMaze(value);
                break;

            case RelativePosition.Right:
                rightWall.SetIsOnMaze(value);
                break;

            case RelativePosition.Down:
                bottomWall.SetIsOnMaze(value);
                break;
            default:
                break;
        }
    }

    public bool GetWall(RelativePosition wallRelativePosition)
    {
        if (walls.ContainsKey(wallRelativePosition))
        {
            return walls[wallRelativePosition];
        }
        throw new Exception("Dictionary does not contain " + wallRelativePosition);

    }


    internal Vector3 GetPosition()
    {
        return transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
