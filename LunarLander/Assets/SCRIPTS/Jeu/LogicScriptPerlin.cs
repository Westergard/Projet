using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScriptPerlin : MonoBehaviour
{
    private static int maxNumTurrets = 3;

    public GameObject target;
    public GameObject turret;
    public Rigidbody2D package;
    public Rigidbody2D bomb;

    public Text score;

    public Vector3 targetPos;
    public Vector3 playerPos;
    public Vector2 playerV;
    public Vector3[] turretPositions = new Vector3[maxNumTurrets];

    public bool packageAllowed = true;
    public bool changeTarget = false;
    public bool bombAllowed = true;

    public float bombDelay = 2.0f;

    public float firstTargetDelay = 1.0f;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Vector3.Distance(playerPos, targetPos) < deliveryDist && packageAllowed)
            {
                sendPackage();
            }else if (bombAllowed)
            {
                dropBomb();
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
    }

    private void sendPackage()
    {
        packageAllowed = false;

        float time = 2.5f;
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
        Instantiate(target, new Vector3(Random.Range(40.0f, 200.0f), Random.Range(75.0f, 150.0f), playerPos.z), Quaternion.identity);
    }

    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        score.text = playerScore.ToString();
    }

    public bool checkPackageTargetDist(Vector3 packagePos)
    {
        return (Vector3.Distance(packagePos, targetPos) < ((2 * packageRadius * packageScale) + (targetRadius * targetScale)));
    }

    public bool checkBombTurretDist(Vector3 bombPos)
    {
        foreach(Vector3 turret in turretPositions)
        {
            if (Vector3.Distance(bombPos, turret) < ((4 * bombRadius * bombScale) + (turretScale * turretRadius)))
            {
                return true;
            }
        }
        return false;
    }

    public bool checkBombTargetDist(Vector3 packagePos)
    {
        return (Vector3.Distance(packagePos, targetPos) < ((4 * bombRadius * bombScale) + (targetRadius * targetScale)));
    }
}