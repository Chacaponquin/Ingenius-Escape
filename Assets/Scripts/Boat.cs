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


}
