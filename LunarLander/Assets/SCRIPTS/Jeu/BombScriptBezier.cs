using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScriptBezier : MonoBehaviour
{
    public LogicScriptBezier logic;
    public Rigidbody2D myRigidBody;

    private int SCORE_FOR_TURRET = 100;

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
            //kill turret
            logic.addScore(SCORE_FOR_TURRET);
        }
        if (logic.checkBombTargetDist(transform.position))
        {
            logic.changeTarget = true;
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
