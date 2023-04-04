using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] options;
    public GameObject tourelle;

    float temps = 1.5f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (temps > 0)
        {
            temps -= Time.deltaTime;
        }
        else
        {
            int randOptions = Random.Range(0, options.Length);

            Instantiate(tourelle, options[randOptions].transform.position, transform.rotation);

            for (int i = 0; i < options.Length; i++)
            {
                Destroy(options[i]);
            }
        }
    }

}
