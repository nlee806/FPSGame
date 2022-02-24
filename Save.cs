using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int score;
    public int health;
    public string name;
    public float timePlayed;
//    public Vector3 location;

    public Save(int scoreInt, int healthInt, string nameStr, float timePlayedF)//, Vector3 locationVect)
    {
        score = scoreInt;
        health = healthInt;
        name = nameStr;
        timePlayed = timePlayedF;
   //     location = locationVect;
    }
}
