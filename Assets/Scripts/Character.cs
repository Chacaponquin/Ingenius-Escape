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
    public CHARACTER_TYPE type;
    public bool isArrival = false;
    public bool destroyed = false;

    public Character(CHARACTER_TYPE type)
    {
        this.type = type;
    }

    public override bool isPlayerOwner(Player player)
    {
        return player.includesCharacter(this);
    }

    public override void move(Cell cell)
    {
    }

    public int value()
    {
        if(type == CHARACTER_TYPE.QUIMICA) {
            return 2;
        }
        else if(type == CHARACTER_TYPE.AUTOMATICA)
        {
            return 3;
        }
        else if (type == CHARACTER_TYPE.INFORMATICA)
        {
            return 3;
        }
        else if (type == CHARACTER_TYPE.INDUSTRIAL)
        {
            return 3;
        }
        else if (type == CHARACTER_TYPE.HIDRAULICA)
        {
            return 1;
        }
        else if (type == CHARACTER_TYPE.ELECTRICA)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public override bool canMove(Cell cell)
    {
        if (actualCell.isLand() && cell.type == CELL_TYPE.WATER && !cell.includeAvailableBoat())
        {
            return false;
        }


        if (actualCell.isLand() && cell.isLand() && !cell.isEmpty())
        {
            return false;
        }

        if(actualCell.type == CELL_TYPE.WATER && !(actualCell.item is Boat) && cell.isLand()) {
            return false;
        }

        if(cell.item is Creature)
        {
            return false;
        }

        return true; 
    }
}


