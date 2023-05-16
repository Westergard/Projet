using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageScriptPerlin : MonoBehaviour
{
    public LogicScriptPerlin logic;

    private int SCORE_FOR_TARGET = 50;

    public Rigidbody2D myRigidBody;
    public float v1;
    public float[] acceleration = new float[100];
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
        //v1 = myRigidBody.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (counter < 100)
        {
            acceleration[counter] = (myRigidBody.velocity.y - v1) / Time.deltaTime;
            v1 = myRigidBody.velocity.y;
            counter++;
        }
        else
        {
            float accelAve = 0;
            foreach(float a in acceleration)
            {
                accelAve += (a / 100);
            }

            counter = 0;
            Debug.Log(accelAve.ToString());
        }
        */

        bords();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(logic.checkPackageTargetDist(transform.position))
        {
            logic.changeTarget = true;
            logic.targetReached = true;
            logic.addScore(SCORE_FOR_TARGET);
            logic.addTime(2);
        }
        Destroy(gameObject);
        logic.packageAllowed = true;
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
