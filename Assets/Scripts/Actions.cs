using System;
using System.Collections;
using System.Collections.Generic;
//rest a little
public static class InputManagerActions
{
    public static Action<int> OnSelectMazeById;
    public static Action<bool> OnEditState;
}

public static class GameActions
{
    public static Action<int[]> OnTheseusMoveRequest;
    public static Action<int[]> OnMinotaurMoveRequest;
    public static Action<GridUnitHandler[,]> OnMazeIsBuilt;
    public static Action OnRestartInput;
}

public static class EditingActions
{
    public static Action<int[]> OnGridUnitHovered;
    public static Action<RelativePosition> OnWallHovered;
    public static Action OnWallNotHovered;
    public static Action<WallStatus> OnWallClicked;
}
