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
    public Text XVelocity;
    public Text YVelocity;

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alt = (gameObject.transform.position.y + 5) * 10;
        Vector2 velocity = myRigidBody.velocity;

        float pushX;
        float pushY;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vaisseauAvecFeu();

            if (myRigidBody.transform.rotation.z < 0.72 && myRigidBody.transform.rotation.z > 0)
            {
                pushX = 1 * Mathf.Sin(myRigidBody.transform.rotation.z);
                pushY = 1 * Mathf.Cos(myRigidBody.transform.rotation.z);

                myRigidBody.velocity = new Vector2(-pushX, pushY);
            }
            if (myRigidBody.transform.rotation.z > -0.72 && myRigidBody.transform.rotation.z < 0)
            {
                pushX = 1 * Mathf.Sin(myRigidBody.transform.rotation.z);
                pushY = 1 * Mathf.Cos(myRigidBody.transform.rotation.z);

                myRigidBody.velocity = new Vector2(-pushX, pushY);
            }
            if(myRigidBody.transform.rotation.z <0.01 && myRigidBody.transform.rotation.z > -0.01)
            {
                myRigidBody.velocity = new Vector2(0, 1);
            }

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            vaisseauSansFeu();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(myRigidBody.transform.rotation.z < 0.71)
            {
               myRigidBody.transform.Rotate(new Vector3(0, 0, (float)0.2));   
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (myRigidBody.transform.rotation.z > -0.71)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)-0.2));
            }
        }

        altitude.text = alt.ToString();
        XVelocity.text = velocity.x.ToString();
        YVelocity.text = velocity.y.ToString();
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
