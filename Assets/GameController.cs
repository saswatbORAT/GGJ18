using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{main,level,over}

public class GameController : MonoBehaviour {

    public GameObject currentContainer;
    public static int score;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GameOver()
    {
        Debug.Log("GameOver");
    }

    void Restart()
    {
        Debug.Log("Restart Level");
    }

    void LevelComplete()
    {
        Debug.Log("Restart Level");
    }
}
