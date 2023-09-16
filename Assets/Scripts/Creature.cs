using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature: CellItem
{
    public override bool canMove(Cell cell)
    {
        if (this.actualCell.isAdjacent(cell))
        {
            if (cell.type == CELL_TYPE.WATER)
            {
                return true;
            }

            return false;
        }
        else
        {
            return false;
        }
    }

    public override bool isPlayerOwner(Player player)
    {
        return true;
    }

}

public class KillerWhale: Creature {
    public override void move(Cell cell)
    {
        cell.charactersToWater();
    }
}

public class Crocodile: Creature {
    public override void move(Cell cell)
    {
        cell.deleteCharacters();
    }
}

public class Dolphin: Creature
{
    public override void move(Cell cell)
    {
    }
}
