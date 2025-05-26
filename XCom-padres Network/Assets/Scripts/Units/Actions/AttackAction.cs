using System;
using System.Collections;
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
            {
                transform.LookAt(enemyUnit.transform);
                unit.animatior.SetBool("Shoot", true);
                StartCoroutine(WaitForAnimation());
            }
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

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(8f);
        enemyUnit.TakeDamage(unit.attack);
        isActive = false;
        onActionComplete();
    }

    public override bool IsAttack()
    {
        return true;
    }
}
