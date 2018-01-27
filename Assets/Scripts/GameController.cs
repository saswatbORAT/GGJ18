using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState{main,level,over}

public class GameController : MonoBehaviour {

    public GameObject currentContainer;
 
    public static int score,life=10;
    public EZObjectPool objectPool;
    public UIController uiController;
    public GameObject powerUI;
    public Text powerTxt;
    // Use this for initialization
	void Start ()
    {
        powerUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

   public  void GameOver()
    {
        Debug.Log("GameOver");
        UIController.levelComplete = false;
        uiController.GameOver();
    }

    public  void Restart()
    {
        Debug.Log("Restart Level");
    }

   public void LevelComplete()
    {
        Debug.Log("Level Complete");
        UIController.levelComplete = true;
        uiController.GameOver();
    }
}
