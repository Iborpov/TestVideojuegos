using System;
using System.Collections.Generic;

public class MoveAction : BaseAction
{
    public override string GetActionName()
    {
        return "Move";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        return new List<GridPosition> { }; //{ unit.GetGridPosition() };
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        throw new NotImplementedException();
    }
}
