using System;
using UnityEngine;


public class MinotaurController: EntityController
{
    public TheseusController theseus;

    protected override void Start()
    {
        base.Start();
        EntitiesActions.OnMinotaurMoveRequest += HandleOnMoveRequest;
    }

    public override void HandleTurn()
    {
        int[] theseusPosition = theseus.GetPosition();
        int[] theseusRelativePosition = new int[2];
        theseusRelativePosition[0] = theseusPosition[0] - currentHorizontalPosition;
        theseusRelativePosition[1] = theseusPosition[1] - currentVerticalPosition;

        //Debug.Log($"Theseus Relative position X: {theseusRelativePosition[0]} Y: {theseusRelativePosition[1]}");

        

        for(int i = 0; i < 2; i++)
        {
            MoveTowardsTheseus(ref theseusRelativePosition);
        }
        



    }

    
    //Function that receives theseus relative position by ref and decrement or 
    //increment based on the success of the movement
    private void MoveTowardsTheseus(ref int[] theseusRelativePosition)
    {
        bool movedHorizontally = false;
        if(theseusRelativePosition[0] > 0)
        {
            if(TryToMove(RelativePosition.Right) == true)
            {
                movedHorizontally = true;
                theseusRelativePosition[0]--;
            }
            
        } 
        
        else if(theseusRelativePosition[0] < 0) 
        {
            if (TryToMove(RelativePosition.Left) == true)
            {
                movedHorizontally = true;
                theseusRelativePosition[0]++;
            }
        } 
        
        if(movedHorizontally == false)
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
        
            

        
        
        

        
    }

    public override bool CheckIfWon()
    {
        int[] theseusPosition = theseus.GetPosition();

        if(theseusPosition[0] == currentHorizontalPosition && theseusPosition[1] == currentVerticalPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
