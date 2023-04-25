using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] options;
    public GameObject tourelle;
    public GameObject tourelle2;
    public GameObject tourelle3;
    public GameObject tourell4;

    public LogicScriptPerlin logic;

    float temps = 1;

    bool spawnTurret = true;


    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temps > 0)
        {
            temps -= Time.deltaTime;
        }
        else if (spawnTurret)
        {
            spawnTurret = false;

            int randOptions = Random.Range(0, options.Length);

            Instantiate(tourelle, options[randOptions].transform.position, transform.rotation);
            logic.turretPosition = options[randOptions].transform.position;
            logic.turretEliminated = false;

            /*
            for (int i = 0; i < options.Length; i++)
            {
                Destroy(options[i]);
            }
            */
        }

        if(temps <= 0)
        {
            foreach (GameObject a in options)
            {
                a.GetComponent<BoxCollider2D>().enabled = false;
                a.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if (logic.changeTurret)
        {
            temps = 1;
            logic.changeTurret = false;
            spawnTurret = true;
        }
    }

}