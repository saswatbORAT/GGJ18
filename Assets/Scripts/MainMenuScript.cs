using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public GameObject LevelObj;

    public void Close(){
        Application.Quit();
    }

    public void Play(){
       int currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
     //  SceneManager.LoadScene("Level_"+currentLevel);
       SceneManager.LoadScene("main");
    }

    public void Levels(){
        LevelObj.SetActive(true);    
    }

    public void BackToMenu(){
        LevelObj.SetActive(false);
    }

    public void OpenLevel(int index){
        SceneManager.LoadScene("Level_" + index); 
    }
}
