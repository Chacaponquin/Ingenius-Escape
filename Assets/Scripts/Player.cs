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
}
