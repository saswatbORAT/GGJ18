using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    GameController gameController;
    private Vector3 velocity = Vector3.one*0.1f;
    public float FollowSpeed = 0.15f;
	// Use this for initialization
	void Awake ()
    {
        gameController = GameObject.FindObjectOfType<GameController>();	

	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetV = new Vector3(target.transform.position.x,0,-10);
        
   //   transform.position = Vector3.SmoothDamp(transform.position, targetV, ref velocity, dampTime);

        transform.position = targetV;
	}
}
