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
    private List<CellItem> items = new List<CellItem>();

    public Cell(GameObject cell, CELL_TYPE type, MapGenerator map) {
        this.gridCell = cell;
        this.type = type;
        this.map = map;
    }

    public bool isLand()
    {
        return this.type == CELL_TYPE.SAND || this.type == CELL_TYPE.DIRT;
    }

    public bool includeBoat()
    {
        bool found = false;

        for(int i = 0; i < this.items.Count && !found; i++) {
            if(this.items[i] is Boat)
            {
                found = true;
            }
        }

        return found;
    }

    public List<Boat> availableBoats() {
        List<Boat> list = new List<Boat>();

        foreach(CellItem c in this.items)
        {
            if(c is Boat)
            {
                Boat boat = (Boat)c;
                if (boat.available())
                {
                    list.Add(boat);
                }
            }
        }

        return list;

    }

    public bool isEmpty()
    {
        return this.items.Count == 0;
    }

    public void setItem(CellItem c)
    {
        this.items.Add(c);
    }

    public bool isAdjacent(Cell cell)
    {
        bool adjacent = false;

        (int x, int y) = this.map.findCellCoords(this);

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

        for(int i =0; i < check.Count && !adjacent; i++)
        {
            CheckCoord coord = check[i];
            Cell foundCell = this.map.getCell(x + coord.x, y + coord.y);

            if(foundCell != null)
            {
                adjacent = true;
            }
        }

        return adjacent;
    }
}

