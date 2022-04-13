using System.Collections;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    protected GridUnitHandler[,] mazeGrid;
    protected int[] currentPosition = new int[2];
    protected int[] positionBeforeMove = new int[2];
    public virtual void HandleTurn() { }
    public virtual bool CheckIfWon() { throw new System.NotImplementedException(); }

    protected virtual void Start()
    {
        RegisterListeners();
    }

    #region EventHandling
    private void RegisterListeners()
    {
        GameActions.OnMazeIsBuilt += HandleOnMazeIsbuilt;
    }

    private void OnDisable()
    {
        GameActions.OnMazeIsBuilt -= HandleOnMazeIsbuilt;
    }

    private void HandleOnMazeIsbuilt(GridUnitHandler[,] mazeGrid)
    {
        this.mazeGrid = mazeGrid;
    }

    protected void HandleOnMoveRequest(int[] goToPosition)
    {
        SetPosition(goToPosition[0], goToPosition[1]);
    }
    #endregion

    #region MovementHandling

    internal bool TryToMove(RelativePosition move)
    {
        int[] incrementPosition = ParseMoveInput(move);

        //Checking if attempted move is outside of limits
        if (CheckIfGridExist(currentPosition[0] + incrementPosition[0], currentPosition[1] + incrementPosition[1]) == false)
        {
            return false;
        }

        GridUnitHandler nextGrid = mazeGrid[currentPosition[0] + incrementPosition[0], currentPosition[1] + incrementPosition[1]];
        //Checking if there is an opposite wall from the neighbour grid unit
        if (nextGrid.GetOppositeWall(move) == true)
        {
            return false;
        }

        //Checking if there is a wall on the current grid unit
        if (mazeGrid[currentPosition[0], currentPosition[1]].GetWall(move) == true)
        {
            return false;
        }

        //if gets here it's because it's a valid move
        Move(incrementPosition[0], incrementPosition[1]);
        return true;
    }
    public virtual void Move(int horizontalMove, int verticalMove)
    {
        SetPosition(currentPosition[0] + horizontalMove, currentPosition[1] + verticalMove);
    }

    internal void UndoMove()
    {
        SetPosition(positionBeforeMove[0], positionBeforeMove[1]);
    }
    public void SetPosition(int x, int y)
    {
        currentPosition[0] = x;
        currentPosition[1] = y;


        transform.position = mazeGrid[currentPosition[0], currentPosition[1]].GetTransformPosition();
    }

    //private IEnumerator MoveAnimated(Vector2 position)
    //{
    //    
    //    //yield return new WaitUntil(() => transform.position-position)
    //}


    public int[] GetPosition()
    {
        return currentPosition;
    }

    #endregion

    #region Utils
    private bool CheckIfGridExist(int nextHorizontalPosition, int nextVerticalPosition)
    {
        if (nextHorizontalPosition > mazeGrid.GetLength(0) - 1
           || nextHorizontalPosition < 0
           || nextVerticalPosition > mazeGrid.GetLength(1) - 1
           || nextVerticalPosition < 0)
        {
            return false;
        }
        return true;
    }
    //Just translating relative position enum to integers values
    protected int[] ParseMoveInput(RelativePosition move)
    {
        int[] movementIncrement = new int[2];

        switch (move)
        {
            case RelativePosition.Up:
                movementIncrement[0] = 0;
                movementIncrement[1] = 1;
                break;

            case RelativePosition.Down:
                movementIncrement[0] = 0;
                movementIncrement[1] = -1;
                break;
            case RelativePosition.Left:
                movementIncrement[0] = -1;
                movementIncrement[1] = 0;
                break;
            case RelativePosition.Right:
                movementIncrement[0] = 1;
                movementIncrement[1] = 0;
                break;
            default:
                break;
        }
        return movementIncrement;
    }
    #endregion

}
