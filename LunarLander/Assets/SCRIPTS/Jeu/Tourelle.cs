using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    public GameObject Vaisseau;
    public GameObject Laser;
    Rigidbody2D myRigidBody;
    Animator m_Animator;
    GameObject newLaser;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Vaisseau != null)
        {
            Vector2 positionVaisseau = Vaisseau.transform.position;
            if (Mathf.Sqrt(Mathf.Pow(positionVaisseau.x - myRigidBody.position.x, 2) + Mathf.Pow(positionVaisseau.y - myRigidBody.position.y, 2)) <= 5)
            {
                if (newLaser == null)
                {
                    newLaser = Instantiate(Laser);
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
            
        }
    }
}


