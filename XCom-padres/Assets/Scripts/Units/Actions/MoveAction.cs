using System;
using System.Collections.Generic;
using System.Linq;

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
        for (int i = -maxMovement; i < maxMovement; i++)
        {
            for (int j = -maxMovement; j < maxMovement; j++)
            {
                if (i != 0 && j != 0)
                {
                    validGridPositions.Append(new GridPosition(gp.x + i, gp.z + j));
                }
            }
        }
        return validGridPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}
