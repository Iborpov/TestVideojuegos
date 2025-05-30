using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    float cellSize;

    //Lista de las celdas
    GridObject[,] gridObjects;

    public GridSystem(int w, int h, float cs)
    {
        this.width = w;
        this.height = h;
        this.cellSize = cs;
        gridObjects = new GridObject[w, h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                gridObjects[i, j] = new GridObject(new GridPosition(i, j));
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public bool IsValidGridPosition(GridPosition position)
    {
        if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    public GridObject GetGridObject(GridPosition gp)
    {
        return gridObjects[gp.x, gp.z];
    }

    public GridObject[,] GetGridObjects()
    {
        return gridObjects;
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
