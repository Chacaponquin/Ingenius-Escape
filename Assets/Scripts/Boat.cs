using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat: CellItem
{
    private const int LIMIT = 3;
    public int id;
    public List<Character> passengers = new List<Character>();

    public Boat(int id)
    {
        this.id = id;
    }

    public bool available() {
        return this.passengers.Count < LIMIT;
    }

    public override void move(Cell cell)
    {
        cell.setItem(this);
        this.setCell(cell);
    }

    public override bool canMove(Cell cell)
    {
        if (!(cell.type == CELL_TYPE.WATER) && !(cell.type == CELL_TYPE.ISLAND))
        {
            return false;
        }

        return true;
    }

    public override bool isPlayerOwner(Player player)
    {
        bool found = false;

        if(this.passengers.Count == 0)
        {
            return false;
        }else
        {
            int count = 0;

            for (int i = 0; i < this.passengers.Count && !found; i++)
            {
                if (player.includesCharacter(this.passengers[i]))
                {
                    count++;
                }
            }

            return count >= this.passengers.Count / 2;
        }
    }


}
