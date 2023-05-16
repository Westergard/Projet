using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScriptBezier : MonoBehaviour
{
    public LogicScriptBezier logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptBezier>();
        logic.targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.changeTarget)
        {
            logic.changeTarget = false;
            Destroy(gameObject);
            logic.spawnTarget();
        }
    }
}
