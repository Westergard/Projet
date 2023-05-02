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

    public float delay = 3;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        map = bezier.GetComponent<MapBezier>();

        delay = 2.5f;
    }

    void Update()
    {
        float timing = 1 * (4.5f - PlayerPrefs.GetInt("Level"));

        if (Vaisseau != null)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                delay = timing;
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


