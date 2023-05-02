using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour {

    [HideInInspector]
    public PathExemeple path;

    public void CreatePath()
    {
        path = new PathExemeple(transform.position);
    }
}
