using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private float t = 1;
    private GameObject explo;
    private GameObject playerExplo;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null){
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Boundary" && other.tag != "Enemy")
        {
            if (explosion != null)
            {
                explo = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
                Destroy(explo, t);
            }
            
            if (other.tag == "Player")
            {
                playerExplo = (GameObject)Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(playerExplo, t);
                gameController.GameOver();
            }
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
