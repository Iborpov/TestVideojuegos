using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField]
    int width;

    [SerializeField]
    int height;

    [SerializeField]
    float cellSize;

    public static LevelGrid Instance { get; private set; }
    public event EventHandler<GridPosition> OnMoveRealized;

    public GridSystem gridSystem;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one LevelGrid Instance");
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(width, height, cellSize);
    }

    void Start() { }

    void Update() { }

    public void AddUnitAtGridPosition(Unit unit)
    {
        GridObject currentGridObject;
        currentGridObject = gridSystem.GetGridObject(unit.GetGridPosition());
        if (currentGridObject != null)
        {
            Debug.Log("Unidad localizada en " + unit.GetGridPosition());
            currentGridObject.AddUnit(unit);
            //OnMoveRealized?.Invoke(this, unit.gridPosition);
        }
        else
        {
            Debug.Log("Intento de añadir unidad en posición sin gridobject");
        }
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition pos)
    {
        return gridSystem.GetGridObject(pos).GetUnitList();
    }

    public List<Unit> GetUnitListAtGridObject(GridObject gridObject)
    {
        return gridObject.GetUnitList();
    }

    public bool HasEnemyOnGridPosition(GridPosition gridPos, Unit allie)
    {
        List<Unit> unitList = GetUnitListAtGridPosition(gridPos);
        foreach (Unit u in unitList)
        {
            if (u.player != allie.player)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveUnitAtGridPosition(Unit unit)
    {
        Debug.Log("Unidad eliminada de posición " + unit.gridPosition);
        GridObject gridObject = gridSystem.GetGridObject(unit.gridPosition);

        if (gridObject != null)
        {
            gridObject.RemoveUnit(unit);
        }
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    public bool IsValidGridPosition(GridPosition mousePosition)
    {
        return gridSystem.IsValidGridPosition(mousePosition);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        bool hasUnits = GetUnitListAtGridPosition(gridPosition).Count > 0;
        return hasUnits;
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition) { }

    public float GetWith()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }

    public List<Unit> GetEnemyUnitsFromGrid(int id)
    {
        //Debug.Log("enemy count:" + GetUnitsFromGrid().Where(x => x.IsEnemy == true).ToList().Count());
        return GetUnitsFromGrid().Where(x => x.player == id).ToList();
    }

    public List<Unit> GetUnitsFromGrid()
    {
        GridObject[,] gridObjects = gridSystem.GetGridObjects();
        List<Unit> units = new List<Unit>();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                units.AddRange(GetUnitListAtGridObject(gridObjects[x, z]));
            }
        }

        return units;
    }
}
