using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecticle : MonoBehaviour {

    GameController gameController;
    public bool isActive;
	// Use this for initialization
	void Awake () 
    {
        gameController = GameObject.FindObjectOfType<GameController>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

      void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
          //  gameObject.SetActive(false);
            isActive = false;
            CameraFollow.target = gameController.currentContainer.transform;
            GameController.life -= 1;
    
            /*if (GameController.life <= 0)
                gameController.GameOver();*/

            GetComponent<Projecticle>().enabled = false;
           // transform.position = gameController.currentContainer.transform.position;
        }
        else  if(other.CompareTag("Player"))
        {
            gameController.currentContainer.GetComponent<Container>().isActive = false;
            gameController.currentContainer = other.gameObject;
            CameraFollow.target = gameController.currentContainer.transform;
            other.GetComponent<Container>().isActive = true;
            gameObject.SetActive(false);
         }
          else  if(other.CompareTag("Collector"))
        {
              
            CameraFollow.target = other.transform;
            UIController.levelComplete = true;
            LeanTween.delayedCall(2, () => {  gameController.LevelComplete(); });
            gameObject.SetActive(false);
         }
    }
}
