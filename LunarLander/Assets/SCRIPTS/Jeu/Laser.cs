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
        Tourelle = GameObject.Find("Tourelle Perlin 1(Clone)");

        Vector2 positionTourelle = Tourelle.transform.position;
        Vector2 positionVaisseau = Vaisseau.transform.position;
        myRigidBody.position = new Vector2(positionTourelle.x, positionTourelle.y + 5f);
        float Xposition = positionVaisseau.x - myRigidBody.position.x;
        float Yposition = positionVaisseau.y - myRigidBody.position.y;
        float Xvelocity = (0.7f)*Xposition / 2;
        float Yvelocity = 0.7f*(((-0.01f) * Mathf.Pow(2, 3) + 2 * Yposition) / (4));
        myRigidBody.velocity = new Vector2(Xvelocity, Yvelocity);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Vaisseau" || c.gameObject.name == "Grass(Clone)")
        {
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
