using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheseusController : EntityController
{

    public override void HandleTurn()
    {
        base.HandleTurn();
    }
    protected override void Start()
    {
        base.Start();
        EntitiesActions.OnTheseusMoveRequest += HandleOnMoveRequest;
    }

    //public override void Move(int horizontalMove, int verticalMove)
    //{
    //    //Debug.Log($"Horizontal move increment {horizontalMove} | Vertical Move Increment {verticalMove}");
    //    SetPosition(currentHorizontalPosition + horizontalMove, currentVerticalPosition + verticalMove);
    //}



    public override bool CheckIfWon()
    {
        return mazeGrid[currentHorizontalPosition, currentVerticalPosition].GetIsExit();
    }

    
}
