using System.Collections.Generic;

public class GridObject
{
    //Posición de la celda
    GridPosition gridPosition;

    //Listado de unidades en la celda
    List<Unit> unitList;

    //Constructor
    public GridObject(GridPosition gp)
    {
        this.gridPosition = gp;
        this.unitList = new List<Unit>();
    }

    //Quita una unidad de la lista de unidades de la celda
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    //Añade una unidad a la lista de unidades de la celda
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    //Devuelve la lista de unidades de la celda
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    //Devuelve la posicion en el grid de la celda
    public GridPosition GetPosition()
    {
        return gridPosition;
    }
}
