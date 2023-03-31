using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaisseauPerlin : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer spriteRenderer;
    public Text altitude;
    public Text XVelocity;
    public Text YVelocity;
    public Text Perdu;
    public Sprite newSprite;
    public AudioSource explosion, reacteur;

    public LogicScriptPerlin logic;

    Animator m_Animator;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptPerlin>();
        m_Animator = gameObject.GetComponent<Animator>();
        Perdu.enabled = false;
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

                myRigidBody.AddForce(new Vector2(5f * -pushX, 8f * pushY));
            }
            if (myRigidBody.transform.rotation.z > -0.72 && myRigidBody.transform.rotation.z < 0)
            {
                pushX = 0.2f * Mathf.Sin(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);
                pushY = 0.2f * Mathf.Cos(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);

                myRigidBody.AddForce(new Vector2(5f * -pushX, 8f * pushY));
            }

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("NFeu");
            reacteur.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("Feu");
        }
        else
        {
            reacteur.Stop();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (myRigidBody.transform.rotation.z < 0.715)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)0.8));

            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (myRigidBody.transform.rotation.z > -0.715)
            {
                myRigidBody.transform.Rotate(new Vector3(0, 0, (float)-0.8));
            }
        }

        altitude.text = alt.ToString("F2");
        XVelocity.text = velocity.x.ToString("F3");
        YVelocity.text = velocity.y.ToString("F3");

        logic.playerPos = transform.position;
        logic.playerV = velocity;

        if (!logic.gameActive)
        {
            altitude.text = "0.00";
            XVelocity.text = "0.000";
            YVelocity.text = "0.000";
            Perdu.enabled = true;
            Destroy(gameObject);
        }
    }

    public void bords()
    {

        if (gameObject.transform.position.x >= 210)
        {
            gameObject.transform.position = new Vector2(29.5f, transform.position.y);
        }
        else if (gameObject.transform.position.x <= 29.5)
        {
            gameObject.transform.position = new Vector2(210, transform.position.y);
        }
        else if (gameObject.transform.position.y >= 158.5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 158.5f);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Grass(Clone)" || c.gameObject.name == "Tourelle" || c.gameObject.name == "Laser(Clone)")
        {
            spriteRenderer.sprite = newSprite;
            gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            m_Animator.SetTrigger("Explosion");
            explosion.Play();
            yield return new WaitForSeconds(2);
            altitude.text = "0.00";
            XVelocity.text = "0.000";
            YVelocity.text = "0.000";
            Perdu.enabled = true;
            Destroy(gameObject);

            logic.timerIsRunning = false;
        }
    }
}
