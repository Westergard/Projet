using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScriptPerlin : MonoBehaviour
{
    public LogicScriptPerlin logic;
    public Rigidbody2D myRigidBody;

    private int SCORE_FOR_TURRET = 100;
    private int SCORE_FOR_TARGET = 25;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
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
            logic.addTime(5);
        }
        if (logic.checkBombTargetDist(transform.position))
        {
            logic.changeTarget = true;
            logic.addScore(SCORE_FOR_TARGET);
        }
        Destroy(gameObject);
    }

    private void bords()
    {

        if (gameObject.transform.position.x >= 210)
        {
            gameObject.transform.position = new Vector2(29.5f, transform.position.y);
        }
        else if (gameObject.transform.position.x <= 29.5)
        {
            gameObject.transform.position = new Vector2(210, transform.position.y);
        }
        else if (gameObject.transform.position.y >= 158.5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 158.5f);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        }
    }


}
