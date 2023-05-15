using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScriptBezier : MonoBehaviour
{
    public LogicScriptBezier logic;
    public Rigidbody2D myRigidBody;
    public AudioSource explosion;

    private int SCORE_FOR_TURRET = 75;
    private int SCORE_FOR_TARGET = 25;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptBezier>();
    }

    void Update()
    {
        bords();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (logic.checkBombTurretDist(transform.position))
        {
            logic.turretEliminated = true;
            logic.changeTurret = true;
            logic.turretPosition = Vector3.zero;
            logic.addScore(SCORE_FOR_TURRET);
            logic.addTime(2);
            explosion.Play();
        }
        if (logic.checkBombTargetDist(transform.position))
        {
            logic.changeTarget = true;
            logic.addScore(SCORE_FOR_TARGET);
        }
        Destroy(gameObject);
    }

    public void bords()
    {

        if (gameObject.transform.position.x >= 9.2)
        {
            gameObject.transform.position = new Vector2(-9.1f, transform.position.y);
        }
        else if (gameObject.transform.position.x <= -9.2)
        {
            gameObject.transform.position = new Vector2(9.1f, transform.position.y);
        }
        else if (gameObject.transform.position.y >= 5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 5);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        }
    }
}
