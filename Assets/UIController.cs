using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Text resultText, resultBtnText;
    public Button resultBtn;
    public GameObject GameOverPanel;

    public int totalLevels;
    int currentLevel;

    void Start(){
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
    }

    public void GameOver(){
        GameOverPanel.SetActive(true);
        if(true){
            resultText.text = "LEVEL COMPLETE";
            resultBtnText.text = "NEXT";
            if (currentLevel == totalLevels)
                resultBtn.interactable = false;
        }else{
            resultText.text = "LEVEL FAILED";
            resultBtnText.text = "RETRY";
        }

        //if game complete, change resultText to level complete and resultBtnText to Retry
         //if game failed, change resultText to level failed and resultBtnText to NEXT
        //if last level and level is complete, disabled the resultBtn
    }

    public void Result(){
        if(true){
            currentLevel++;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            SceneManager.LoadScene("Level_" + currentLevel);
        }else{
            SceneManager.LoadScene("Level_" + currentLevel);
        }
    }

    public void GotoMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
