using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecticle : MonoBehaviour {

    GameController gameController;

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
            gameObject.SetActive(false);
            transform.position = gameController.currentContainer.transform.position;
        }
    }
}
