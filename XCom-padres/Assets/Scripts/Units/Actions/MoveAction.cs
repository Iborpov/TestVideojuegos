using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (true)
        {
            isActive = false;
            onActionComplete();
        }
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition gp = unit.GetGridPosition();
        return new List<GridPosition> { };
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
