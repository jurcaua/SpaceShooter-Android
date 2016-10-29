using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed;
    public float speedInc;

    private Rigidbody rb;
    private Rigidbody player;
    private GameController gameScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameScript = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").GetComponent<Rigidbody>();
        }
        else
        {
            player = null;
        }

        if (transform.tag == "Bolt" && player)
        {
            rb.velocity = new Vector3
                (
                    player.velocity.x,
                    0.0f,
                    Mathf.Clamp(player.velocity.z, 0.0f, 100)
                ) + transform.forward * speed;

            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }

        else if (transform.parent && transform.parent.name == "Background")
        {
            rb.velocity = -transform.up * speed;
        }

        else
        {
            rb.velocity = transform.forward * (speed + speedInc * gameScript.numWaves);
        }
    }
}
