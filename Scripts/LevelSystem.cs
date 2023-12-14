using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private int level;
    private int experience;
    private int experienceToNextLevel;

    //Level System default values
    public LevelSystem() { 
    
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
 
    }

    //Add an amount of exp and manage level up
    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel) 
        {
            level++;
            experience -= experienceToNextLevel;
        }
    }
}
