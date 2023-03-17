using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.killTarget)
        {
            Destroy(gameObject);
            logic.killTarget = false;
        }
    }
}
