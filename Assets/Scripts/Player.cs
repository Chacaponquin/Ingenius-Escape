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
                if(character.actualCell.type == CELL_TYPE.WATER && character.actualCell.item is Boat)
                {
                    returnCharacters.Add(character);
                }
            }
        }

        return returnCharacters;
    }

    public List<Character> hidraulicsInWater()
    {
        List<Character> returnCharacters = new List<Character>();

        foreach (Character character in this.characters)
        {
            if (character.actualCell != null)
            {
                if (character.actualCell.type == CELL_TYPE.WATER && character.actualCell.item == character)
                {
                    returnCharacters.Add(character);
                }
            }
        }

        return returnCharacters;
    }
}
