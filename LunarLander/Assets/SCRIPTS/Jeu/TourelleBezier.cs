using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleBezier : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject Vaisseau;
    public GameObject Laser;
    Animator m_Animator;
    GameObject newLaser;
    MapBezier map;
    public GameObject bezier;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        map = bezier.GetComponent<MapBezier>();
    }

    void Update()
    {
        
        if (Vaisseau != null)
        {
            if (newLaser == null)
            {
                newLaser = Instantiate(Laser);
                audioSource.Play();
            }
            if (Vaisseau.transform.position.x > gameObject.transform.position.x)
            {
                //m_Animator.SetTrigger("Droite");
                transform.rotation = Quaternion.Euler(0, 180, -map.PenteTourette);
            }
            else if (Vaisseau.transform.position.x < gameObject.transform.position.x)
            {
                //m_Animator.SetTrigger("Gauche");
                //map.Tourne180Degreer = 0f;
                //map.PenteTourette *= -1;
                transform.rotation = Quaternion.Euler(0, 0f, map.PenteTourette);
            }
        }
    }
}


