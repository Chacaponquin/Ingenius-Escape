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

        return true; 
    }
}


