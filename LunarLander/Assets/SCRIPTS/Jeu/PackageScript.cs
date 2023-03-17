using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageScript : MonoBehaviour
{
    public LogicScript logic;
    public TargetScript target;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<TargetScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(Vector3.Distance(transform.position, logic.targetPos) < 0.25f)
        {
            logic.changeTarget();
            logic.addScore(5);
        }
        
        Destroy(gameObject);
        logic.packageAllowed = true;
    }
}
