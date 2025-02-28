using System;
using System.Collections.Generic;

public class MoveAction : BaseAction
{
    int maxMovement = 3;

    List<GridPosition> movePositions;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (true)
        {
            unit.animatior.SetBool("IsRunning", true);
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
        List<GridPosition> validGridPositions = new List<GridPosition>();
        //for (int i = 0; i < maxMovement; i++) { }
        return validGridPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
