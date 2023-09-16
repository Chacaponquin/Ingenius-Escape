using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTER_TYPE
{
    AUTOMATICA,
    MECANICA,
    QUIMICA,
    HIDRAULICA,
    ELECTRICA,
    INFORMATICA,
    INDUSTRIAL
}

public class Character: CellItem
{
    public int id;
    public Cell actualCell;
    public CHARACTER_TYPE type;

    public bool canMove(Cell cell)
    {
        if (actualCell.isAdjacent(cell))
        {
            if (actualCell.isLand() && cell.type == CELL_TYPE.WATER)
            {
                return false;
            }


            if (cell.type == CELL_TYPE.WATER && cell.includeBoat())
            {
                List<Boat> allBoats = cell.availableBoats();
                return !(allBoats.Count == 0);
            }

            return true;
        }
        else
        {
            return false;
        }        
    }
}


