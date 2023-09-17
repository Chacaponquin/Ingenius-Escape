using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature: CellItem
{
    public override bool canMove(Cell cell)
    {
        if (!cell.isEmpty() && cell.item is Creature)
        {
            return false;
        }


        return this.canMoveCreature(cell);
    }

    protected abstract bool canMoveCreature(Cell cell);

    public override bool isPlayerOwner(Player player)
    {
        return true;
    }

}

public class KillerWhale: Creature {
    public override void move(Cell cell)
    {
        cell.destroyBoats();
    }

    protected override bool canMoveCreature(Cell cell)
    {
        if(cell.type == CELL_TYPE.WATER && cell.item is Character)
        {
            return false;
        }

        return true;
    }
}

public class Crocodile: Creature {
    public override void move(Cell cell)
    {
        cell.deleteCharacters();
    }

    protected override bool canMoveCreature(Cell cell)
    {
        if (cell.type == CELL_TYPE.WATER && cell.item is Boat)
        {
            return false;
        }

        return true;
    }
}

public class Dolphin: Creature
{
    public override void move(Cell cell)
    {
        cell.destroyAll();
    }

    protected override bool canMoveCreature(Cell cell)
    {
        if(cell.type == CELL_TYPE.WATER)
        {
            return true;
        }

        return true;
    }
}
