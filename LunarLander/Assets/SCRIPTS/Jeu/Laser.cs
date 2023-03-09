using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject Vaisseau;
    private GameObject Tourelle;
    public Rigidbody2D myRigidBody;

    void Start()
    {
        Vaisseau = GameObject.Find("Vaisseau");
        Tourelle = GameObject.Find("Tourelle");

        Vector2 positionTourelle = Tourelle.transform.position;
        Vector2 positionVaisseau = Vaisseau.transform.position;
        myRigidBody.position = new Vector2(positionTourelle.x, positionTourelle.y + 0.1f);
        float Xposition = positionVaisseau.x - myRigidBody.position.x;
        float Yposition = positionVaisseau.y - myRigidBody.position.y;
        float Xvelocity = Xposition / 3;
        float Yvelocity = ((-0.01f) * Mathf.Pow(2, 2) + 2 * Yposition) / (2 * 2); ;
        myRigidBody.velocity = new Vector2(Xvelocity, Yvelocity);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Vaisseau" || c.gameObject.name == "tile(Clone)")
        {
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
