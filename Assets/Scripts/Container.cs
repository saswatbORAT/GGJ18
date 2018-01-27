using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour {

    GameObject projectile, powerUI;

    [SerializeField][Range(0,50)]
    float maxShootForce;
    [SerializeField][Range(0,200)]
    float dragDistance;


    float shootForce,power;
    private float angle;
    Vector2 initPos, lastPos;
    Rigidbody2D projectileRB;
    Quaternion targetRotation;
    Text powerTxt;
    Transform spawnPoint;
    bool canShoot = true;
    // Use this for initialization
	void Awake ()
    {
       projectile = GameObject.Find("Projectile");
       powerUI =  GameObject.Find("PowerHUD");
       spawnPoint = transform.GetChild(0);
       projectileRB = projectile.GetComponent<Rigidbody2D>();
       powerTxt = powerUI.GetComponentInChildren<Text>();
       projectile.SetActive(false);
       powerUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
        {
            initPos = Input.mousePosition;

            powerUI.SetActive(true);
        }
        else if (Input.GetMouseButton(0) && canShoot)
        {
            lastPos = Input.mousePosition;

            Vector2 distance = (lastPos - initPos);
            Vector2 tempAngle = distance.normalized;
     
            angle = Vector3.Angle(tempAngle, Vector3.up);
             //For 360 degree angle
             if (tempAngle.x > 0)
                 angle = 360 - angle;
 
            
            transform.rotation = Quaternion.Euler(0, 0, angle-90);

            power = Mathf.Clamp( (distance.magnitude / dragDistance),0,1);
            shootForce = (power * maxShootForce);
            powerTxt.text = "Power " + (int)(power * 100) + "Angle " + (int)angle;
            Debug.Log("shoot "+shootForce+"power "+power);
        }
        else if(Input.GetMouseButtonUp(0) && canShoot)
        {
            Shoot();
        }
      
	}

    void Shoot()
    {
        projectile.SetActive(true);
        projectile.transform.position = spawnPoint.transform.position;
        projectileRB.velocity = Vector2.zero;
        Debug.Log(shootForce);
        projectileRB.AddForce(transform.right*shootForce,ForceMode2D.Impulse);
    }
    
 
}
