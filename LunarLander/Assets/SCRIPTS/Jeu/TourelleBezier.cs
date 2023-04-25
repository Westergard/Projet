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
        
        if (Vaisseau!= null)
        {
            if (newLaser == null)
            {
                newLaser = Instantiate(Laser);
                audioSource.Play();
            }
            if (Vaisseau.transform.position.x > gameObject.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, -map.PenteTourette); // On tourne de 180 en Y donc la pente est devenue négative.
            }
            else if (Vaisseau.transform.position.x < gameObject.transform.position.x)
            {    
                transform.rotation = Quaternion.Euler(0, 0f, map.PenteTourette);
            }
        }
    }
}


