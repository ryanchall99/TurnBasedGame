using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{

    private GridSystem gridSystem;
    private GridPosition gridPosition;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem; // System Grid Is Using
        this.gridPosition = gridPosition; // Grid Position Of Object
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
