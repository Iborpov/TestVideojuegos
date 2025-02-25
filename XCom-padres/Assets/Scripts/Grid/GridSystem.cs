using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    float width;
    float height;

    //Lista de las celdas
    List<GridObject>[] gridObjects;

    public GridSystem(float w, float h)
    {
        this.width = w;
        this.height = h;
    }

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

    public GridObject GetGridObjet(GridPosition gp)
    {
        return gridObjects[gp.x][gp.z];
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
