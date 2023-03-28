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
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(logic.checkPackageTargetDist(transform.position))
        {
            logic.changeTarget = true;
            logic.addScore(SCORE_FOR_TARGET);
        }
        Destroy(gameObject);
        logic.packageAllowed = true;
    }
}
