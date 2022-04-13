using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    protected GridUnitHandler[,] mazeGrid;
    protected int currentHorizontalPosition;
    protected int currentVerticalPosition;
    public virtual void HandleTurn() { }
    public virtual bool CheckIfWon() { throw new System.NotImplementedException(); }

    protected virtual void Start()
    {
        EntitiesActions.OnMazeIsBuilt += HandleOnMazeIsbuilt;
    }

    protected void HandleOnMoveRequest(int[] goToPosition)
    {
        SetPosition(goToPosition[0], goToPosition[1]);
    }

    private void HandleOnMazeIsbuilt(GridUnitHandler[,] mazeGrid)
    {
        //Debug.Log("Setting Handle Maze Is Built");
        Debug.Log(mazeGrid[0, 0].transform.name);
        this.mazeGrid = mazeGrid;
        //Debug.Log("Sanity check");
        //Debug.Log(this.mazeGrid[0, 0].transform.name);
    }
    public virtual void Move(int horizontalMove, int verticalMove)
    {
        SetPosition(currentHorizontalPosition + horizontalMove, currentVerticalPosition + verticalMove);
    }

    
    public int[] GetPosition()
    {
        int[] currentPosition = new int[2];
        currentPosition[0] = currentHorizontalPosition;
        currentPosition[1] = currentVerticalPosition;
        return currentPosition;
    }

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

    internal bool TryToMove(RelativePosition move)
    {
        int[] incrementPosition = ParseMoveInput(move);


        if (CheckIfGridExist(currentHorizontalPosition + incrementPosition[0], currentVerticalPosition + incrementPosition[1]) == false)
        {
            return false;
        }


        GridUnitHandler nextGrid = mazeGrid[currentHorizontalPosition + incrementPosition[0], currentVerticalPosition + incrementPosition[1]];
        
        if (nextGrid.GetOppositeWall(move) == true)
        {
            return false;
        }
        

        if (mazeGrid[currentHorizontalPosition, currentVerticalPosition].GetWall(move) == true)
        {
            return false;
        }
        
        Move(incrementPosition[0], incrementPosition[1]);
        return true;
    }

    public void SetPosition(int x, int y)
    {
        Debug.Log($"The current Horizontal Position was {currentHorizontalPosition}");

        currentHorizontalPosition = x;
        currentVerticalPosition = y;

        //Debug.Log($"The new Horizontal Position is {currentHorizontalPosition}");

        //currentUnitGrid = mazeGrid[x, y];
        Debug.Log(mazeGrid[0, 0].transform.name);
        transform.position = mazeGrid[currentHorizontalPosition, currentVerticalPosition].GetPosition();
    }

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

}
