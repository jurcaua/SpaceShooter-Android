using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tiltx, tilty;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float shotSpeed;
    public float fireRate;
    public float startWait;

	public float rotationForce;
	public float dodgeForce;
	public float dodgeTime;
	public float dodgeRate;

    private float nextFire;
	private float nextDodge;
    private Rigidbody rb;
	private ConstantForce cf;
    private AudioSource audioSource;
    
    private GameController gameScript;
	private TrailRenderer trail1;
	private TrailRenderer trail2;

    void Start()
    {
        //rigidbody
		rb = GetComponent<Rigidbody>();
		// constant force
		cf = GetComponent<ConstantForce>();
        //audio
        audioSource = GetComponent<AudioSource>();
        // gamecontroller ref
        gameScript = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		// getting the two trails
		trail1 = GameObject.Find("Trail Spawn 1").GetComponent<TrailRenderer>();
		trail2 = GameObject.Find("Trail Spawn 2").GetComponent<TrailRenderer>();
		trail1.enabled = false;
		trail2.enabled = false;
    }

    void Update()
    {
		if (CrossPlatformInputManager.GetButton("Shoot") && Time.time > startWait && Time.time > nextFire && gameScript.restart == false)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 0, 0));
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
		//handling movement
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

//			GYROSCOPE
//			Vector3 acceleration = Input.acceleration;
//			Vector3 movement = new Vector3(acceleration.x, 0.0f, -acceleration.z);
		rb.velocity = movement * speed;

		rb.rotation = Quaternion.Euler (rb.velocity.z * tilty, rb.velocity.x, rb.velocity.x * -tiltx);

		if (Time.time > nextDodge) {
			if (CrossPlatformInputManager.GetButton ("LDodge")) 
			{
				StartCoroutine (Dodge (-dodgeForce, -rotationForce));
			}

			if (CrossPlatformInputManager.GetButton ("RDodge")) 
			{
				StartCoroutine (Dodge (dodgeForce, rotationForce));
			}
		}

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }

	IEnumerator Dodge(float dodgeForce, float rotationForce)
	{
		trail1.enabled = true;
		trail2.enabled = true;
		rb.velocity = Vector3.zero; 																			// set volecity to zero
		cf.force = new Vector3 (dodgeForce, 0.0f, 0.0f); 	   												   // the dash
		transform.Rotate (new Vector3(0, rotationForce/3, -rotationForce)); 								  // the rotation
		yield return new WaitForSeconds(dodgeTime);		     												 // wait during the dodge
		cf.force = Vector3.zero; 						    												// no more dash
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 0); 		   // no more rotation
		nextDodge = Time.time + dodgeRate; 			 	  												  // can only dodge again after the cooldown
	}
}
