using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckCoord
{
    public int x;
    public int y;

    public CheckCoord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public enum CELL_TYPE
{
    WATER,
    SAND,
    DIRT,
    ISLAND
}
public class Cell
{
    public GameObject gridCell;
    public CELL_TYPE type;
    public MapGenerator map;
    public CellItem item = null;
    public GameObject image = null;
    public int x;
    public int y;

    public Cell(GameObject cell, CELL_TYPE type, MapGenerator map, int x, int y) {
        this.gridCell = cell;
        this.type = type;
        this.map = map;

        this.x = x;
        this.y = y;
    }

    public void setImage(GameObject g)
    {
        this.image=g;
    }

    public bool isLand()
    {
        return this.type == CELL_TYPE.SAND || this.type == CELL_TYPE.DIRT;
    }

    public void charactersToWater()
    {
        if(item is Boat)
        {
            this.item = null;
        }
    }

    public void deleteCharacters()
    {
        if (this.item is Character)
        {
            this.item = null;
        }
    }

    public bool includeBoat()
    {
        return this.item is Boat;
    }

    public bool isEmpty()
    {
        return this.item == null;
    }

    public void setItem(CellItem c)
    {
        this.item = c;
    }

    public bool isAdjacent(Cell cell)
    {
        bool adjacent = false;

        List<CheckCoord> check = new List<CheckCoord>();
        // vertical
        check.Add(new CheckCoord(0, -1));
        check.Add(new CheckCoord(0, 1));

        // horizontal
        check.Add(new CheckCoord(-1, 0));
        check.Add(new CheckCoord(1, 0));

        // diagonal left
        check.Add(new CheckCoord(-1, -1));
        check.Add(new CheckCoord(1, 1));

        // diagonal right
        check.Add(new CheckCoord(1, -1));
        check.Add(new CheckCoord(-1, 1));

        for(int i = 0; i < check.Count && !adjacent; i++)
        {
            CheckCoord coord = check[i];
            Cell foundCell = this.map.getCell(this.x + coord.x, this.y + coord.y);

            if(foundCell != null &&  foundCell == cell)
            {
                adjacent = true;
            }
        }

        return adjacent;
    }
}

