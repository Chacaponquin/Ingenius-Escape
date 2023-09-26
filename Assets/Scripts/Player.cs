using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum COLOR{
    YELLOW,
    RED,
    BLUE
}
public class Player
{
    public string name;
    public COLOR color;
    public List<Character> characters = new List<Character>();
    
    public Player(string name, COLOR color)
    {
        this.name = name;
        this.color = color;
    }

    public void setCharacter(Character c)
    {
        this.characters.Add(c);
    }

    public bool includesCharacter(Character c)
    {
        return this.characters.IndexOf(c) != -1;
    }

    public List<Character> mechanicsInBoats()
    {
        List<Character> returnCharacters = new List<Character>();

        foreach(Character character in this.characters)
        {
            if(character.actualCell != null)
            {
                if (character.type == CHARACTER_TYPE.MECANICA)
                {
                    Debug.Log(character.actualCell);
                    Debug.Log(character.actualCell.item);
                    if (character.actualCell.item is Boat)
                    {
                        Boat boat = (Boat)character.actualCell.item;

                        if (boat.includes(character)) {
                            returnCharacters.Add(character); 
                        }
                    }
                }
            }
        }

        return returnCharacters;
    }

    public int points()
    {
        int sum = 0;

        foreach (Character c in this.characters)
        {
            if (c.isArrival)
            {
                sum += c.value();
            }
        }

        return sum;
    }

    public List<Character> hidraulicsInWater()
    {
        List<Character> returnCharacters = new List<Character>();

        foreach (Character character in this.characters)
        {
            if (character.actualCell != null)
            {
                if (character.actualCell.type == CELL_TYPE.WATER && character.actualCell.item == character && character.type == CHARACTER_TYPE.HIDRAULICA)
                {
                    returnCharacters.Add(character);
                }
            }
        }

        return returnCharacters;
    }
}
