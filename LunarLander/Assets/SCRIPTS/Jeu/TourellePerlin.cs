using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourellePerlin : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject Vaisseau;
    public GameObject Laser;
    public LogicScriptPerlin logic;
    Animator m_Animator;
    GameObject newLaser;
    public AudioSource explosion;

    public float delay = 3;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        Vaisseau = GameObject.Find("Vaisseau");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();

        delay = 2.5f;
    }

    void Update()
    {
        float timing = 1 * (4.5f - PlayerPrefs.GetInt("Level"));

        //Fait apparaitre les lasers à un certain interval de temps
        if (logic.gameActive && !logic.turretEliminated)
        {
            if(delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                delay = timing;
                newLaser = Instantiate(Laser);
                audioSource.Play();
            }

            //Tourner selon la position en x du vaisseau
            if (Vaisseau.transform.position.x > gameObject.transform.position.x)
            {
                m_Animator.SetTrigger("Droite");
            }
            else if (Vaisseau.transform.position.x < gameObject.transform.position.x)
            {
                m_Animator.SetTrigger("Gauche");
            }
        }

        if (logic.turretEliminated)
        {
            Destroy(gameObject);
        }
    }
}


