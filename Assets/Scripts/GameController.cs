using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText shootText;
    public GUIText joystickText;

    public bool restart; //; public so PlayerController can access and not fire a shot right when restarting
    public int numWaves;
    private int score;
    private bool gameOver;
    private bool start;

    void Start()
    {
        // before we start spawning waves we gotta do somethings
        gameOverText.text = "Space Shooter";
        scoreText.text = "";
        restartText.text = "Tap the Screen to Start";
        shootText.text = "Tap or Hold (Shoot)";
        joystickText.text = "Joystick (Move)";
        start = false;
    }

    void Update()
    {
        if (Time.time > 0)
        {
            if (start == false && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0)))
            {
                start = true;
                score = 0;
                gameOver = false;
                restart = false;
                restartText.text = "";
                gameOverText.text = "";
                shootText.text = "";
                joystickText.text = "";
                UpdateScore();
                numWaves = 0;
                StartCoroutine(SpawnWaves());
            }
        }
        

        if (restart)
        {
            if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
                    || Input.GetMouseButtonUp(0)) // just for restarting in unity game window
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            hazardCount += 2;
            numWaves += 1;
            /*
            if (spawnWait > 0.05)
            {
                spawnWait -= 0.03f;
            }
            if (waveWait > 1)
            {
                waveWait -= 0.5f;
            }*/

            if (gameOver)
            {
                restartText.text = "Tap to Return to Menu";
                restart = true;
                break;
            }
        }       
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
