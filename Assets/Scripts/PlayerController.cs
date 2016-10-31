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

    private float nextFire;
    private Rigidbody rb;
    private AudioSource audioSource;
    
    private GameController gameScript;

    void Start()
    {
        //rigidbody
		rb = GetComponent<Rigidbody>();
        //audio
        audioSource = GetComponent<AudioSource>();
        // gamecontroller ref
        gameScript = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
		if (CrossPlatformInputManager.GetButton("Action") && Time.time > startWait && Time.time > nextFire && gameScript.restart == false)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 0, 0));
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
		//handling movement
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//		Vector3 acceleration = Input.acceleration;
//		Vector3 movement = new Vector3(acceleration.x, 0.0f, -acceleration.z);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(rb.velocity.z * tilty, rb.velocity.x, rb.velocity.x * -tiltx);

		//handling dodging

    }

}
