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

        if (logic.gameActive && !logic.turretEliminated)
        {
            if (newLaser == null)
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

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Caca")
        {
            spriteRenderer.sprite = newSprite;
            //gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            m_Animator.SetTrigger("Explosion");
            explosion.Play();
            //yield return new WaitForSeconds(1);
        }
    }
}


