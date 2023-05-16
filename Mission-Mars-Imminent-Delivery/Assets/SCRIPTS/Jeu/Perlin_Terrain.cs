using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin_Terrain : MonoBehaviour
{
    public GameObject bType = null;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 750f; i++)
        {
            GameObject bx = GameObject.Instantiate(bType);

            float xPosition = -9.8f + i / 18f;
            float yPosition = -9f + Mathf.PerlinNoise(i / 90f, 0f) * 6f;

            bx.transform.position = new Vector3(xPosition, yPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
