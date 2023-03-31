using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandom : MonoBehaviour
{
    public float x;
    public float y;

    public TileRandom()
    {

    }

    public TileRandom(float x, float y)
    {

    }

    public void setx(float nx)
    {
        x = nx;
    }

    public void sety(float ny)
    {
        y = ny;
    }

    public int getx()
    {
        return x;
    }

    public int gety()
    {
        return y;
    }
}
