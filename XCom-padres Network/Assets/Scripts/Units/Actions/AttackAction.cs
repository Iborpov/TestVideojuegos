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

        if (true)
        {
            if (IsServer)
                unit.animatior.SetBool("Shoot", true);
            enemyUnit.TakeDamage(unit.attack);
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
        int range = unit.attackRange;
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitPosition = unit.gridPosition;
        Debug.Log("GetValidGridPositionList from " + unitPosition);
        for (int x = unitPosition.x - range; x <= unitPosition.x + range; x++)
        {
            for (int z = unitPosition.z - range; z <= unitPosition.z + range; z++)
            {
                GridPosition testingPosition = new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testingPosition))
                {
                    Debug.Log("Attack descarte por invalid position");
                    continue;
                }

                if (testingPosition == unitPosition)
                {
                    Debug.Log("Attack descarte por same position");
                    continue;
                }

                var enemies = LevelGrid.Instance.GetUnitListAtGridPosition(testingPosition);
                if (enemies.Count == 0)
                {
                    Debug.Log("Attack descarte por no unidades. " + testingPosition);
                    continue;
                }
                if (unit.player == enemies[0].player)
                {
                    Debug.Log("Attack descarte por no enemigos");
                    continue;
                }

                validGridPositions.Add(testingPosition);
            }
        }

        //Filtra las posiciones para que solo haya el maximo posible en todas las direcciones
        /*validGridPositions = validGridPositions.FindAll(
            (GridPosition gridPosition) =>
            {
                Vector3 yIgnore = Vector3.down + Vector3.one;
                float dist = Vector3.Distance(
                    Vector3.Scale(LevelGrid.Instance.GetWorldPosition(gridPosition), yIgnore),
                    Vector3.Scale(transform.position, yIgnore)
                );
                return dist / Mathf.Sqrt(3) <= range + 0.1f;
            }
        );*/
        return validGridPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        List<Unit> enemyUnits = LevelGrid
            .Instance.gridSystem.GetGridObject(gridPosition)
            .GetUnitList();

        if (enemyUnits == null)
            return;
        enemyUnit = enemyUnits[0];
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
