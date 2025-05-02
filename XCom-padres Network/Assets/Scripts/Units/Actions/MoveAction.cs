using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAction : BaseAction
{
    [SerializeField]
    NavMeshAgent navMesh;
    int maxMovement = 2;

    GridPosition actionPosition;
    List<GridPosition> movePositions;

    private void Update()
    {
        if (!isActive)
        {
            if (IsServer)
                unit.animatior.SetBool("IsRunning", false);
            return;
        }

        if (isActive)
        {
            if (IsServer)
                unit.animatior.SetBool("IsRunning", true);
            navMesh.destination = LevelGrid.Instance.GetWorldPosition(actionPosition);
            if (
                Vector2.Distance(
                    new Vector2(unit.transform.position.x, unit.transform.position.z),
                    new Vector2(
                        LevelGrid.Instance.GetWorldPosition(actionPosition).x,
                        LevelGrid.Instance.GetWorldPosition(actionPosition).z
                    )
                ) <= .1
            )
            {
                isActive = false;
                onActionComplete();
            }
        }
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition gp = unit.GetGridPosition();
        NavMeshPath path = new NavMeshPath();
        List<GridPosition> validGridPositions = new List<GridPosition>();
        for (int x = gp.x + -maxMovement; x < gp.x + maxMovement + 1; x++)
        {
            for (int z = gp.z + -maxMovement; z < gp.z + maxMovement + 1; z++)
            {
                GridPosition posible = new GridPosition(x, z);

                if (navMesh.CalculatePath(LevelGrid.Instance.GetWorldPosition(posible), path))
                {
                    if (posible != gp)
                    {
                        validGridPositions.Add(posible);
                    }
                }
            }
        }
        return validGridPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        actionPosition = gridPosition;
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
