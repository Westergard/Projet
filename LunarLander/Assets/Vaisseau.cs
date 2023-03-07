using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaisseau : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Text altitude;
    public Text XVelocity;
    public Text YVelocity;
    public Collider collider;
    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float alt = (gameObject.transform.position.y + 5) * 10;
        Vector2 velocity = myRigidBody.velocity;
        bords();
        float pushX;
        float pushY;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (myRigidBody.transform.rotation.z < 0.72 && myRigidBody.transform.rotation.z > 0)
            {
                pushX = 0.2f * Mathf.Sin(((myRigidBody.transform.rotation.z * 90f) / 0.72f) * Mathf.Deg2Rad);
                pushY = 0.2f * Mathf.Cos(((myRigidBody.transform.rotation.z * 90f) / 0.72f) * Mathf.Deg2Rad);

                myRigidBody.AddForce(new Vector2(0.6f * -pushX, 1f * pushY));
            }
            if (myRigidBody.transform.rotation.z > -0.72 && myRigidBody.transform.rotation.z < 0)
            {
                pushX = 0.2f * Mathf.Sin(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);
                pushY = 0.2f * Mathf.Cos(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);

                myRigidBody.AddForce(new Vector2(0.6f * -pushX, 1f * pushY));
            }

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("NFeu");
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("Feu");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(myRigidBody.transform.rotation.z < 0.715)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)1));

            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (myRigidBody.transform.rotation.z > -0.715)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)-1));
            }
        }

        altitude.text = alt.ToString("F2");
        XVelocity.text = velocity.x.ToString("F3");
        YVelocity.text = velocity.y.ToString("F3");
    }

    public void bords()
    {

        if(gameObject.transform.position.x >= 9.2)
        {
            gameObject.transform.position = new Vector2(-9.1f, transform.position.y);
        }
        else if (gameObject.transform.position.x <= -9.2)
        {
            gameObject.transform.position = new Vector2(9.1f, transform.position.y);
        }
        else if (gameObject.transform.position.y >= 5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 5);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "tile(Clone)")
        {
            m_Animator.SetTrigger("Explosion");
            Destroy(gameObject);
        }
        if (c.gameObject.name == "Tourelle")
        {
            m_Animator.SetTrigger("Explosion");
            Destroy(gameObject);
        }
    }
}
