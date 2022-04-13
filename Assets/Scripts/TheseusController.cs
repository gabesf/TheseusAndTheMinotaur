using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheseusController : EntityController
{

    #region Utils
    protected override void Start()
    {
        base.Start();
        GameActions.OnTheseusMoveRequest += HandleOnMoveRequest;
    }

    private void OnDisable()
    {
        GameActions.OnTheseusMoveRequest -= HandleOnMoveRequest;
    }

    public override void HandleTurn()
    {
        base.HandleTurn();
    }
    public override bool CheckIfWon()
    {
        return mazeGrid[currentPosition[0], currentPosition[1]].GetIsExit();
    }


    #endregion

    #region MovementHandling

    //Must override Move because the entities stores positionBeforeMove differently
    public override void Move(int horizontalMove, int verticalMove)
    {
        positionBeforeMove[0] = currentPosition[0];
        positionBeforeMove[1] = currentPosition[1];
        base.Move(horizontalMove, verticalMove);
    }

    #endregion

}
