using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D package;

    public GameObject target;
    public GameObject turret;

    public Text scoreLabel;

    public Vector3 targetPos;
    public Vector3 playerPos;
    public Vector2 playerV;

    public float magnitudeOffset = 0.32f;

    public int score = 0;

    public bool packageAllowed = true;
    public bool killTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(target, new Vector3(0, 0, player.transform.position.z), Quaternion.identity);
        //spawn turret
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Vector3.Distance(playerPos, targetPos) < 10)
            {
                if (packageAllowed)
                {
                    sendPackage();
                }
            }
        }
    }

    private void sendPackage()
    {
        float time = 1.5f;
        float acceleration = -0.103581861f;
        float v_x = ((targetPos.x - playerPos.x) / time);
        float v_y = ((((targetPos.y - playerPos.y) - (0.5f * time * time * acceleration))) / time);
        Vector3 posOffset = (targetPos - playerPos) / (Vector3.Distance(targetPos, playerPos) / magnitudeOffset);

        Rigidbody2D packageInstance = Instantiate(package, playerPos + posOffset, Quaternion.identity);
        packageInstance.velocity = new Vector2(v_x, v_y);
        packageAllowed = false;
    }

    public void changeTarget()
    {
        killTarget = true;
        Instantiate(target, new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-5.0f, 5.0f), player.transform.position.z), Quaternion.identity);
    }

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreLabel.text = score.ToString();
    }
}
