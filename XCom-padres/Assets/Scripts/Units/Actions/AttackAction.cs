using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;

        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        if (true)
        {
            isActive = false;
            onActionComplete();
        }
    }

    public override string GetActionName()
    {
        return "Attack";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        throw new NotImplementedException();
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
