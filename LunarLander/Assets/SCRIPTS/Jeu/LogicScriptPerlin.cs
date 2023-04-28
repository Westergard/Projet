using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScriptPerlin : MonoBehaviour
{
    private static int maxNumTurrets = 3;

    public GameObject[] options;
    public GameObject target;
    public GameObject turret;
    public Rigidbody2D package;
    public Rigidbody2D bomb;

    public Text score;
    public Text time;

    public Vector3 targetPos;
    public Vector3 playerPos;
    public Vector2 playerV;
    public Vector3 turretPosition;
    public Vector3 ciblePosition;

    public bool packageAllowed = true;
    public bool changeTarget = false;
    public bool targetReached = false;
    public bool bombAllowed = true;
    public bool timerIsRunning = true;
    public bool gameActive = true;
    public bool turretEliminated = false;
    public bool cibleEliminated = false;
    public bool changeTurret = false;
    public bool checkStats = true;
    public bool scoreHigher = false;
    public bool timeHigher = false;
    public bool spawnCible = false;

    public float bombDelay = 1.5f;

    public float firstTargetDelay = 1.0f;

    public float timeRemaining = 90.0f;
    public float timePlayed = 0;

    public int playerScore = 0;

    public float deliveryDist = 20.0f;

    public float shipRadius = 3.168f;
    public float shipScale = 1.719646f;
    public float packageRadius = 0.1f;
    public float packageScale = 1.0f;
    public float bombRadius = 4.34f;
    public float bombScale = 0.36f;
    public float targetRadius = 0.2f;
    public float targetScale = 10.5f;
    public float turretRadius = 1.25f;
    public float turretScale = 6.15f;



    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        gameActive = true;

        if (PlayerPrefs.HasKey("Level"))
        {
            bombDelay *= PlayerPrefs.GetInt("Level");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameActive)
            {
                if(Vector3.Distance(playerPos, targetPos) < deliveryDist && packageAllowed)
                {
                    sendPackage();
                }
                else if (bombAllowed)
                {
                    dropBomb();
                }
            }
        }

        if (!bombAllowed)
        {
            bombDelay -= Time.deltaTime;
            if(bombDelay <= 0)
            {
                bombDelay = 2;
                bombAllowed = true;
            }
        }

        if (firstTargetDelay > 0)
        {
            firstTargetDelay -= Time.deltaTime;
            if(firstTargetDelay <= 0)
            {
                spawnTarget();
            }
        }

        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                adjustTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                gameActive = false;
            }
        }

        if (gameActive)
        {
            timePlayed += Time.deltaTime;
        }

        if(checkStats && !gameActive)
        {
            checkStats = false;

            if(PlayerPrefs.HasKey("high score"))
            {
                if(playerScore > PlayerPrefs.GetInt("high score"))
                {
                    PlayerPrefs.SetInt("high score", playerScore);
                }
            }
            else
            {
                PlayerPrefs.SetInt("high score", playerScore);
            }

            if(PlayerPrefs.HasKey("high time"))
            {
                if(timePlayed > PlayerPrefs.GetFloat("high time"))
                {
                    PlayerPrefs.SetFloat("high time", timePlayed);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("high time", timePlayed);
            }
        }

        if(!scoreHigher && playerScore > PlayerPrefs.GetInt("high score"))
        {
            score.color = Color.yellow;
            scoreHigher = true;
        }

        if (!timeHigher && timePlayed > PlayerPrefs.GetFloat("high time"))
        {
            time.color = Color.yellow;
            timeHigher = false;
        }
    }

    //le code pour le "timer" est pris d'Internet
    //https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#code_example
    private void adjustTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void sendPackage()
    {
        packageAllowed = false;

        float time = 1;

        if (PlayerPrefs.HasKey("Level"))
        {
            time *= PlayerPrefs.GetInt("Level");
        }
        
        float acceleration = -0.101547565f;
        float v_x = ((targetPos.x - playerPos.x) / time);
        float v_y = ((targetPos.y - (playerPos.y + (0.5f * time * time * acceleration))) / time);

        float distOffset = (shipRadius * shipScale) + (packageRadius * packageScale);
        float velocityMagnitude = Mathf.Sqrt(v_x * v_x + v_y * v_y);
        Vector3 trajectoryOffset3 = new Vector3(v_x * distOffset / velocityMagnitude, v_y * distOffset / velocityMagnitude, 0);

        v_x = ((targetPos.x - (playerPos.x + trajectoryOffset3.x)) / time);
        v_y = ((targetPos.y - ((playerPos.y + trajectoryOffset3.y) + (0.5f * time * time * acceleration))) / time);

        Rigidbody2D packageInstance = Instantiate(package, playerPos + trajectoryOffset3, Quaternion.identity);
        packageInstance.velocity = new Vector2(v_x, v_y);
    }

    private void dropBomb()
    {
        bombAllowed = false;

        Vector3 bombOffset = Vector3.down * ((shipRadius * shipScale) + (bombRadius * bombScale));

        Rigidbody2D bombInstance = Instantiate(bomb, playerPos + bombOffset, Quaternion.identity);
        bombInstance.velocity = playerV;
    }

    public void spawnTarget()
    {
        if (firstTargetDelay > 0)
        {
            firstTargetDelay -= Time.deltaTime;
        }
        else if (spawnCible)
        {
            spawnCible = false;

            int randOptions = Random.Range(0, options.Length);

            Instantiate(target, options[randOptions].transform.position, transform.rotation);
            ciblePosition = options[randOptions].transform.position;
            cibleEliminated = false;

            /*
            for (int i = 0; i < options.Length; i++)
            {
                Destroy(options[i]);
            }
            */
        }

        if (firstTargetDelay <= 0)
        {
            foreach (GameObject a in options)
            {
                a.GetComponent<BoxCollider2D>().enabled = false;
                a.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        if (cibleEliminated)
        {
            spawnTarget();
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        score.text = playerScore.ToString();
    }

    public void addTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
    }

    public bool checkPackageTargetDist(Vector3 packagePos)
    {
        return (Vector3.Distance(packagePos, targetPos) <= (1.1 * ((packageRadius * packageScale) + (targetRadius * targetScale))));
    }

    public bool checkBombTurretDist(Vector3 bombPos)
    {
        return (Vector3.Distance(bombPos, turretPosition) <= (1.35 * ((bombRadius * bombScale) + (turretRadius * turretScale))));
    }

    public bool checkBombTargetDist(Vector3 bombPos)
    {
        return (Vector3.Distance(bombPos, targetPos) <= (1.35 * ((bombRadius * bombScale) + (targetRadius * targetScale))));
    }
}
