using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    float width;
    float height;

    List<GridObject>[] gridObjects;

    public Vector3 GetWoldPosition(GridPosition gridPosition)
    {
        return LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return LevelGrid.Instance.GetGridPosition(worldPosition);
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        throw new NotImplementedException();
    }

    public GridObject GetGridObjet()
    {
        throw new NotImplementedException();
    }

    public float GetWith()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }
}
