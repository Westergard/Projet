using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaisseau : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    private SpriteRenderer rend;
    public Sprite VAF, VSF;
    public Text altitude;

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alt = (gameObject.transform.position.y + 5) * 10;

        float pushX;
        float pushY;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vaisseauAvecFeu();
            pushX = 1 * Mathf.Sin(myRigidBody.transform.rotation.z);
            pushY = 1 * Mathf.Cos(myRigidBody.transform.rotation.z);
            myRigidBody.velocity = new Vector3(pushX, pushY, 0);

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            vaisseauSansFeu();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(myRigidBody.transform.rotation.z < 90)
            {
               myRigidBody.transform.Rotate(new Vector3(0, 0, (float)0.2));   
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (myRigidBody.transform.rotation.z > -90)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)-0.2));
            }
        }

        altitude.text = alt.ToString();
    }

    void vaisseauAvecFeu()
    {
        rend.sprite = VAF;
    }
    void vaisseauSansFeu()
    {
        rend.sprite = VSF;
    }
}
