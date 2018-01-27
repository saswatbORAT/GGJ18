using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static Transform target;
    GameController gameController;
    private Vector3 velocity = Vector3.one*0.1f;
    public float FollowSpeed = 0.5f;
	  public  Transform initialTarget;

      bool isTweening;
    // Use this for initialization
	void Awake ()
    {
        target = initialTarget;
        gameController = GameObject.FindObjectOfType<GameController>();	

	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetV = new Vector3(target.transform.position.x,0,-10);
        
        if(!target.CompareTag("Projectile"))
        transform.position = Vector3.SmoothDamp(transform.position, targetV, ref velocity, FollowSpeed);
        else
        transform.position = targetV;
	}

    
}
