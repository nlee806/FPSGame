using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        SaveLoad.LoadFile();
        bool error = SaveLoad.CheckError();
        if (error == true)
        {
            Debug.Log("Starting New Game");
            SceneManager.LoadScene("SampleScene");
        }
        else {
            int health = SaveLoad.getHealth();
            string level = SaveLoad.getLevel();
            Vector3 location = SaveLoad.getLocation();
            int score = SaveLoad.getScore();
            float time = SaveLoad.getTime();
            Debug.Log("Loading Saved Game");
      
            SceneManager.LoadScene("SampleScene"); //TODO: put new variables into scene

        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Debug.Log("Saving Game");
        SaveGame();
        Debug.Log("Game Saved");

        Application.Quit();
    }
    public void SaveGame() {
        //Save();
        SaveLoad.SaveFile();
        Debug.Log("File Saved!");
    }

   
}
