using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBezier : MonoBehaviour
{
    private GameObject Vaisseau;
    private GameObject Tourelle;
    public Rigidbody2D myRigidBody;

    void Start()
    {
        Vaisseau = GameObject.Find("VaisseauBezier");
        Tourelle = GameObject.Find("Tourelle Bézier");

        Vector2 positionTourelle = Tourelle.transform.position;
        Vector2 positionVaisseau = Vaisseau.transform.position;
        myRigidBody.position = new Vector2(positionTourelle.x, positionTourelle.y + 0.5f);
        float Xposition = positionVaisseau.x - myRigidBody.position.x;
        float Yposition = positionVaisseau.y - myRigidBody.position.y;
        float Xvelocity = 0;
        float Yvelocity = 0;

        if (PlayerPrefs.GetInt("Level") == 1)
        {
            Xvelocity = (0.5f) * Xposition / 2;
            Yvelocity = (0.5f) * (((-0.01f) * Mathf.Pow(2, 3) + 2 * Yposition) / (4));
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            Xvelocity = Xposition / 2;
            Yvelocity = (((-0.01f) * Mathf.Pow(2, 3) + 2 * Yposition) / (4));
        }
        else if (PlayerPrefs.GetInt("Level") == 3)
        {
            Xvelocity = (1.5f) * Xposition / 2;
            Yvelocity = (1.5f) * (((-0.01f) * Mathf.Pow(2, 3) + 2 * Yposition) / (4));
        }
        else if (PlayerPrefs.GetInt("Level") == 4)
        {
            Xvelocity = (2) * Xposition / 2;
            Yvelocity = (2) * (((-0.01f) * Mathf.Pow(2, 3) + 2 * Yposition) / (4));
        }
        myRigidBody.velocity = new Vector2(Xvelocity, Yvelocity);
        transform.rotation = Quaternion.Euler(0f, 0f, CalculePente(myRigidBody.position, positionVaisseau));
    }

    void OnCollisionEnter2D(Collision2D c)
    {
            Destroy(gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    float CalculePente(Vector2 Point1, Vector2 Point2)
    {
        float DeltaY, DeltaX, Pent;
        DeltaX = Point1.x - Point2.x;
        DeltaY = Point1.y - Point2.y;
        Pent = DeltaY / DeltaX;

        return (Mathf.Atan(Pent) * 180 / 3.1416f) + 90;//de RAD en DEG
    }
}
