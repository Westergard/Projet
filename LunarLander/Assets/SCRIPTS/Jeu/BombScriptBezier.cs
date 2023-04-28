using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScriptBezier : MonoBehaviour
{
    public LogicScriptBezier logic;
    public Rigidbody2D myRigidBody;

    private int SCORE_FOR_TURRET = 75;
    private int SCORE_FOR_TARGET = 25;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptBezier>();
    }

    // Update is called once per frame
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
