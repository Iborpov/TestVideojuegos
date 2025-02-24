using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridObject
{
    GridPosition gridPosition;

    List<Unit> unitList;

    public void RemoveUnit() { }

    public void AddUnit(Unit unit)
    {
        unitList.Append(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }
}
