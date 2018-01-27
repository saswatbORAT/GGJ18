using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour {

    GameObject projectile, powerUI;

    
    [SerializeField][Range(0,50)]
    float touchRadius;
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
    Transform knob;

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
           knob = transform.GetChild(0).transform.GetChild(0);
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
            canShoot = false;
            initPos = Input.mousePosition;
           float touchDistance = Vector2.Distance(transform.position ,Camera.main.ScreenToWorldPoint(initPos));
            Debug.Log(touchDistance);
            
            if (touchDistance <= touchRadius)
            {
                dots.gameObject.SetActive(true);
                canShoot= true;
            }
            box.enabled = false; 
  
            powerUI.SetActive(true);
        }
        else if (Input.GetMouseButton(0) && canShoot)
        {
            lastPos = Input.mousePosition;

            Vector2 distance = (lastPos - initPos);
            Vector2 tempAngle = distance.normalized;
     
           angle = Vector3.Angle(tempAngle, -Vector3.right);
             //For 360 degree angle
           if (Mathf.Sin(tempAngle.y) > 0)
               angle = 360 - angle;
          
              

            Vector3 forwardVector = angle * Vector3.forward;
            float radianAngle = Mathf.Atan2(forwardVector.z, forwardVector.x);
            float degreeAngle = radianAngle * Mathf.Rad2Deg;

            if(angle >= minimumRotation && angle <=maximumRotation)
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
               
            }
           
            power = Mathf.Clamp( (distance.magnitude / dragDistance),0,1);
            knob.transform.localPosition =new Vector2(0, -power*0.85f);
            powerTxt.text = "Power " + (int)(power * 100) + "Angle " + (int)angle;
            shootForce = (power * maxShootForce);
            dots.startSpeed = 2 * power;
           
         
        //    UpdateTrajectory(transform.position,new Vector3(shootForce,5,0),new Vector3(0,-9.8f,0));
          //  Debug.Log("angle "+ angle+"angle"+Mathf.Sin(tempAngle.y));
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
      //  projectileRB.velocity = Vector2.zero;
        Debug.Log(shootForce);
        knob.transform.localPosition = Vector2.zero;
        dots.gameObject.SetActive(false);
        LeanTween.delayedCall(1, () => { box.enabled = true; });
		//box.enabled = true;
        projectileRB.AddForce(transform.right*shootForce,ForceMode2D.Impulse);
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
