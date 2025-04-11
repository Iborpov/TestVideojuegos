using System;
using System.Collections.Generic;

public class GranadeAction : BaseAction
{
    public override string GetActionName()
    {
        return "Granade";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        throw new NotImplementedException();
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        throw new NotImplementedException();
    }
}
