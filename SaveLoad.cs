using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    static int currentScore = 0;
    static int currentHealth = 0;
    static string currentName = "Level 0";
    static float currentTimePlayed = 5.0f;
    static Vector3 location; //leave Vector3 hardcoded!
    static bool error;
    static int newcurrentScore = 0;
    static int newcurrentHealth = 0;
    static string newcurrentName = "Level 0";
    static float newcurrentTimePlayed = 5.0f;
    static Vector3 newlocation; //leave Vector3 hardcoded!


    static void Start()
    {
        SaveFile();// currentScore, currentHealth, currentName, currentTimePlayed);
        LoadFile();
        location = new Vector3(0,0,0);
        newlocation = new Vector3(-8.6f,3.6f,9.9f);
        error = false;
    }

    public static void SaveFile()//int score, int health, string name, float time)
    {/*
        currentScore = score;
        currentHealth = health;
        currentName = name;
        currentTimePlayed = time;
       */
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        Save data = new Save(currentScore, currentHealth, currentName, currentTimePlayed);//, location);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            error = true;
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Save data = (Save)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        currentHealth = data.health;
        currentName = data.name;
        currentTimePlayed = data.timePlayed;
 //       location = data.location;
        newcurrentScore = data.score;
        newcurrentHealth = data.health;
        newcurrentName = data.name;
        newcurrentTimePlayed = data.timePlayed;
 //       newlocation = data.location;

        Debug.Log(data.name);
        Debug.Log(data.score);
        Debug.Log(data.health);
        Debug.Log(data.timePlayed);
 //       Debug.Log(data.location);
    }
    public static void SaveData(int score, int health, string name, float time)
    {
        currentScore = score;
        currentHealth = health;
        currentName = name;
        currentTimePlayed = time;
    }
    public static void SaveLocation(Vector3 mylocation)
    {
        location = mylocation;
    }
    public static bool CheckError() {
        return error;
    }
    public static int getScore() {
        return newcurrentScore;
    }
    public static int getHealth()
    {
        return newcurrentHealth;
    }
    public static string getLevel()
    {
        return newcurrentName;
    }
    public static float getTime()
    {
        return newcurrentTimePlayed;
    }
    public static Vector3 getLocation() 
        //DO NOT USE THIS FUNCTION-Vector3 is NOT SERIALIZABLE
    {
        return newlocation;
    }
}

/*
    public static List<Game> savedGames = new List<Game>();

    //it's static so we can call it from anywhere
    public static void Save()
    {
        SaveLoad.savedGames.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }*/