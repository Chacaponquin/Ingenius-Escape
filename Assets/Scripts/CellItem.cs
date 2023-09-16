using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellItem
{
    public Cell actualCell;

    public void setCell(Cell cell)
    {
        this.actualCell = cell;
    }

    public abstract bool isPlayerOwner(Player player);
    public abstract bool canMove(Cell cell);
    public abstract void move(Cell cell);

    public void moveToCell(Cell cell)
    {
        this.setCell(cell);
        cell.setItem(this);
        this.move(cell);
    }

    public bool canMoveToCell(Cell cell)
    {
        if (this.actualCell.isAdjacent(cell)){
            if (!cell.isEmpty())
            {
                return false;
            }

            return this.canMove(cell);
        }else
        {
            return false;
        }
    }
}
