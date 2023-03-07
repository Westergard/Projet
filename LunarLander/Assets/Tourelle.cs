using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    public GameObject Vaisseau;
    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(Vaisseau.transform.position.x > gameObject.transform.position.x)
        {
            m_Animator.SetTrigger("Droite");
        }
        else if(Vaisseau.transform.position.x < gameObject.transform.position.x)
        {
            m_Animator.SetTrigger("Gauche");
        }
    }
}
