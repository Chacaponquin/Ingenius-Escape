using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CELL_TYPE
{
    WATER,
    SAND,
    DIRT
}
public class Cell
{
    public GameObject gridCell;
    private CELL_TYPE type;
    private bool isActive;

    public Cell(GameObject cell, CELL_TYPE type) {
        this.gridCell = cell;
        this.type = type;
    }
}

