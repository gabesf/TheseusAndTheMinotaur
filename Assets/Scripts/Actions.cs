using System;
using System.Collections;
using System.Collections.Generic;

public static class InputManagerActions
{
    public static Action<int> OnSelectMazeById;
}

public static class EntitiesActions
{
    public static Action<int[]> OnTheseusMoveRequest;
    public static Action<int[]> OnMinotaurMoveRequest;
    public static Action<GridUnitHandler[,]> OnMazeIsBuilt;
}
