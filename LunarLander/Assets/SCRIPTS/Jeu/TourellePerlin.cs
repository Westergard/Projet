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
    private LineRenderer Line;
    private bool vue = true;
    EdgeCollider2D edgeCollider;

    void Start()
    {
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        Line = GetComponent<LineRenderer>();
        m_Animator = gameObject.GetComponent<Animator>();
        Vaisseau = GameObject.Find("Vaisseau");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
        //for (int i = 0; i < 100; i++)
        //{
            //p[i] = Instantiate(point);
        //}
    }

    void Update()
    {
        Vector2 positionTourelle = transform.position;
        Vector2 positionVaisseau = Vaisseau.transform.position;
        Line.SetPosition(0, new Vector2(positionTourelle.x, positionTourelle.y + 6f));
        Line.SetPosition(1,positionVaisseau);

        List<Vector2> col = new List<Vector2>();
        col.Add(positionTourelle);
        col.Add(positionVaisseau);
        edgeCollider.SetPoints(col);
        edgeCollider.isTrigger = true;




        //float a = (positionVaisseau.y - (positionTourelle.y + 6f))/(positionVaisseau.x - positionTourelle.x);
        //float b = (positionVaisseau.y - (a * positionVaisseau.x));

        //float longueurLigne = Mathf.Sqrt(Mathf.Pow(positionVaisseau.x - positionTourelle.x, 2) + Mathf.Pow(positionVaisseau.y - (positionTourelle.y + 6f), 2));
        //float distXEntrePoints = (positionVaisseau.x - positionTourelle.x)/100;

        //for(int i = 0; i < 100; i++)
        //{
        // p[i].transform.position = new Vector2(positionTourelle.x + i * distXEntrePoints, (a * positionTourelle.x + (i + 1) * distXEntrePoints) + b);
        //}


        if (logic.gameActive && !logic.turretEliminated)
        {
            if (newLaser == null && vue)
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
            //spriteRenderer.sprite = newSprite;
            //gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            m_Animator.SetTrigger("Explosion");
            explosion.Play();
            //yield return new WaitForSeconds(1);
        }
    }
}


