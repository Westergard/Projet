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

    public float delay = 1;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        Vaisseau = GameObject.Find("Vaisseau");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
    }

    void Update()
    {
        if (logic.gameActive && !logic.turretEliminated)
        {
            if(PlayerPrefs.GetInt("Level") == 1 || PlayerPrefs.GetInt("Level") == 2 || PlayerPrefs.GetInt("Level") == 3)
            {
                if (newLaser == null)
                {
                    newLaser = Instantiate(Laser);
                    audioSource.Play();
                }
            }
            if(PlayerPrefs.GetInt("Level") == 4)
            {
                if(delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    delay = 1;
                    newLaser = Instantiate(Laser);
                    audioSource.Play();
                }
            }
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


