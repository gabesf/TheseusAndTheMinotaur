using System;
using UnityEngine;


public class MinotaurController: EntityController
{
    public TheseusController theseus;

    protected override void Start()
    {
        base.Start();
        GameActions.OnMinotaurMoveRequest += HandleOnMoveRequest;
    }



    #region Movement Handling
    //Function that receives theseus relative position by ref and decrement or 
    //increment based on the success of the movement
    private void MoveTowardsTheseus(ref int[] theseusRelativePosition)
    {
        bool movedHorizontally = false;
        movedHorizontally = TryToMoveHorizontally(theseusRelativePosition, movedHorizontally);

        //Minotaur will only attempt to move vertically if didn't moved horizontally
        if (movedHorizontally == false)
        {
            TryToMoveVertically(theseusRelativePosition);
        }
    }

    private bool TryToMoveHorizontally(int[] theseusRelativePosition, bool movedHorizontally)
    {
        if (theseusRelativePosition[0] > 0)
        {
            if (TryToMove(RelativePosition.Right) == true)
            {
                movedHorizontally = true;
                theseusRelativePosition[0]--;
            }
        }

        else if (theseusRelativePosition[0] < 0)
        {
            if (TryToMove(RelativePosition.Left) == true)
            {
                movedHorizontally = true;
                theseusRelativePosition[0]++;
            }
        }
        return movedHorizontally;
    }
    private void TryToMoveVertically(int[] theseusRelativePosition)
    {
        if (theseusRelativePosition[1] > 0)
        {
            if (TryToMove(RelativePosition.Up) == true)
            {
                theseusRelativePosition[1]--;
            }
        }

        else if (theseusRelativePosition[1] < 0)
        {
            if (TryToMove(RelativePosition.Down) == true)
            {
                theseusRelativePosition[1]++;
            }
        }
    }

    #endregion


    #region Round Handling
    public override void HandleTurn()
    {
        int[] theseusPosition = theseus.GetPosition();
        int[] theseusRelativePosition = new int[2];
        theseusRelativePosition[0] = theseusPosition[0] - currentPosition[0];
        theseusRelativePosition[1] = theseusPosition[1] - currentPosition[1];

        //Minotaur must store the position before any of the possible moves
        positionBeforeMove[0] = currentPosition[0];
        positionBeforeMove[1] = currentPosition[1];


        for (int i = 0; i < 2; i++)
        {
            MoveTowardsTheseus(ref theseusRelativePosition);
        }
    }

    public override bool CheckIfWon()
    {
        int[] theseusPosition = theseus.GetPosition();

        if (theseusPosition[0] == currentPosition[0] && theseusPosition[1] == currentPosition[1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    
}
