using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public float xPos, yPos;
    public bool[] collectables;
    public int completions;

    public GameData()
    {
        xPos = 0;
        yPos = 0;
        collectables = new bool[15];
        completions = 0;
    }
}
