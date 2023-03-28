using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public LogicScript logic;
    public Rigidbody2D myRigidBody;

    private int SCORE_FOR_TURRET = 100;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

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
}
