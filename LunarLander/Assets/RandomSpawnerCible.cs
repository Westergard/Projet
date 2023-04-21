using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerCible : MonoBehaviour
{
    public GameObject[] options;
    public GameObject cible;

    public LogicScriptPerlin logic;

    float temps = 1.5f;

    bool spawnCible = true;


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
        else if (spawnCible)
        {
            spawnCible = false;

            int randOptions = Random.Range(0, options.Length);

            Instantiate(cible, options[randOptions].transform.position, transform.rotation);
            logic.ciblePosition = options[randOptions].transform.position;
            logic.cibleEliminated = false;



            /*
            for (int i = 0; i < options.Length; i++)
            {
                Destroy(options[i]);
            }
            */
        }

        if (temps <= 0)
        {
            foreach (GameObject a in options)
            {
                a.GetComponent<BoxCollider2D>().enabled = false;
                a.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if (logic.changeCible)
        {
            temps = 1;
            logic.changeCible = false;
            spawnCible = true;
        }
    }
}
