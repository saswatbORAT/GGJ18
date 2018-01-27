using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour {

    GameObject projectile, powerUI;


    [SerializeField][Range(0,50)]
    float maxShootForce;
    [SerializeField][Range(0,100)][Tooltip("Give in % 1 to 100. ")]
    float minShootForce;
    [SerializeField][Range(0,200)]
    float dragDistance;
    [SerializeField][Range(0,360)]
    float minimumRotation;
    [SerializeField][Range(0,360)]
    float maximumRotation;
    [SerializeField][Range(0,10)]
    float animSpeed;
    [SerializeField][Range(0,10)]
    float verticalDistance;

    [SerializeField][Range(0,10)]
    float horizontalDistance;

    float shootForce,power;
    private float angle;
    Vector2 initPos, lastPos;
    Rigidbody2D projectileRB;
    Quaternion targetRotation;
    Text powerTxt;
    Transform spawnPoint;
    bool canShoot = true;
    public AnimationCurve curve;
    public bool isActive;
    private GameController gameController;
    private BoxCollider2D box;
    private ParticleSystem dots;
    // Use this for initialization
	void Awake ()
    {
        box = GetComponent<BoxCollider2D>();
      
        if(isActive)
        {
           projectile = GameObject.Find("Projectile");
           powerUI =  GameObject.Find("PowerHUD");
           spawnPoint = transform.GetChild(0);
           projectileRB = projectile.GetComponent<Rigidbody2D>();
       
           powerTxt = powerUI.GetComponentInChildren<Text>();
           projectile.SetActive(false);
           powerUI.SetActive(false);
               box.enabled = false;
        }
         gameController = GameObject.FindObjectOfType<GameController>();
         dots = GetComponentInChildren<ParticleSystem>();
         dots.gameObject.SetActive(false);
	}
	
    void OnEnable()
    {
      //  LeanTween.moveX(gameObject,transform.position.x+horizontalDistance,horizontalDistance/animSpeed).setLoopPingPong().setEase(curve);
    //    LeanTween.moveY(gameObject,transform.position.y+verticalDistance,verticalDistance/animSpeed).setLoopPingPong().setEase(curve);
    }
	// Update is called once per frame
	void Update () 
    {
       if(isActive)
       {

        if(Input.GetMouseButtonDown(0))
        {
            initPos = Input.mousePosition;
            box.enabled = false; 
            dots.gameObject.SetActive(true);
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
 
            if(angle-90 >= minimumRotation && angle-90 <=maximumRotation)
            {
                transform.rotation = Quaternion.Euler(0, 0, angle-90);
               
            }
           
            power = Mathf.Clamp( (distance.magnitude / dragDistance),0,1);
            powerTxt.text = "Power " + (int)(power * 100) + "Angle " + (int)angle;
            shootForce = (power * maxShootForce);
            dots.startSpeed = 2 * power;
           
         
        //    UpdateTrajectory(transform.position,new Vector3(shootForce,5,0),new Vector3(0,-9.8f,0));
            Debug.Log("shoot "+shootForce+"power "+power);
        }
        else if(Input.GetMouseButtonUp(0) && canShoot)
        {
            if(power*100 > minShootForce)
            Shoot();
        }
       }
	}

    void Shoot()
    {
          box.enabled = false;
        projectile.SetActive(true);
        projectile.transform.position = spawnPoint.transform.position;
        projectileRB.velocity = Vector2.zero;
        Debug.Log(shootForce);
           dots.gameObject.SetActive(false);
        projectileRB.AddForce(transform.right*shootForce,ForceMode2D.Impulse);
    }
    
     void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 20; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example
 
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);
 
        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        for (int i = 0; i < numSteps; ++i)
        {
            lineRenderer.SetPosition(i, position);
 
            position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
            velocity += gravity * timeDelta;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Projectile") )
        {
            GameObject[] containers = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject g in containers)
            {
                g.GetComponent<Container>().isActive = false;
            }
           this.isActive = true;
           gameController.currentContainer = gameObject;
           projectile = GameObject.Find("Projectile");
           powerUI =  GameObject.Find("PowerHUD");
           spawnPoint = transform.GetChild(0);
           projectileRB = projectile.GetComponent<Rigidbody2D>();
           powerTxt = powerUI.GetComponentInChildren<Text>();
           projectile.SetActive(false);
           powerUI.SetActive(false);
        }
    }
}
