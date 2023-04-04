using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] options;
    public GameObject tourelle;

    bool enCollision;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < options.Length; i++)
        {
            while (enCollision = false)
            {
                options[i].position = new Vector2(transform.position.x, (transform.position.y - 0.01f));
            }

            enCollision = false;
        }

        int randOptions = Random.Range(0, options.Length);

        Instantiate(tourelle, options[randOptions].position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        enCollision = true;
    }

}