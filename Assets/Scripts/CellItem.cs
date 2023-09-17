using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellItem
{
    public Cell actualCell;
    public bool disable = false;

    public void setCell(Cell cell)
    {
        this.actualCell = cell;
    }

    public abstract bool isPlayerOwner(Player player);
    public abstract bool canMove(Cell cell);
    public abstract void move(Cell cell);

    public void moveToCell(Cell cell)
    {
        if(this is Character && cell.item is Boat)
        {
            this.actualCell.setItem(null);
            this.actualCell.image.SetActive(false);
            this.actualCell.image = null;
            Boat b = (Boat)cell.item;
            b.passengers.Add((Character)this);
        }
        else if (this is Creature) {
            this.move(cell);
            this.actualCell.image.transform.position = cell.gridCell.transform.position;
            cell.image = actualCell.image;
            this.actualCell.setItem(null);

            this.setCell(cell);
            cell.setItem(this);
        }
        else
        {
            this.actualCell.image.transform.position = cell.gridCell.transform.position;
            cell.image = actualCell.image;
            this.actualCell.setItem(null);

            this.setCell(cell);
            cell.setItem(this);

            this.move(cell);
        }

        

    }

    public bool canMoveToCell(Cell cell)
    {
        bool isAdjacent = this.actualCell.isAdjacent(cell);

        if (isAdjacent)
        {
            return this.canMove(cell);
        }
        else
        {
            return false;
        }
    }
}
