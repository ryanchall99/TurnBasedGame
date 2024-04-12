using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height; // "Length" Used Height Due To Visualising in 2D
    private float cellSize;

    public GridSystem(int width, int height, float cellSize) 
    {
        // Setting Variables On Construction
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        for (int x = 0; x < width; x++) 
        {
            for (int z = 0; z < height; z++) 
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }
}
