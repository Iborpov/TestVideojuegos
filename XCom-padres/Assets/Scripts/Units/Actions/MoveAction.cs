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
        for (int x = gp.x + -maxMovement; x < maxMovement; x++)
        {
            for (int z = gp.z + -maxMovement; z < maxMovement; z++)
            {
                validGridPositions.Add(new GridPosition(x, z));
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
