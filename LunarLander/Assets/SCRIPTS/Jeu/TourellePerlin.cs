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

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        Vaisseau = GameObject.Find("Vaisseau");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
    }

    void Update()
    {
        int vue = PlayerPrefs.GetInt("Vue");

        if (logic.gameActive && !logic.turretEliminated)
        {
            if (newLaser == null && vue == 0)
            {
                newLaser = Instantiate(Laser);
                audioSource.Play();
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


