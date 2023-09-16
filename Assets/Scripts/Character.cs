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
    public CHARACTER_TYPE type;

    public Character(int id, CHARACTER_TYPE type)
    {
        this.id = id;
        this.type = type;
    }

    public override bool isPlayerOwner(Player player)
    {
        return player.includesCharacter(this);
    }

    public override void move(Cell cell)
    {
        cell.setItem(this);
        this.actualCell = cell;
    }

    public override bool canMove(Cell cell)
    {
        if (actualCell.isLand() && cell.type == CELL_TYPE.WATER)
        {
            return false;
        }


        if (cell.type == CELL_TYPE.WATER && cell.includeBoat())
        {
            Boat boat = (Boat)cell.item;
            return boat.available();
        }

        return true; 
    }
}


