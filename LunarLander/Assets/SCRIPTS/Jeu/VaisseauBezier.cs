using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaisseauBezier : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer spriteRenderer;
    public Text altitude;
    public Text XVelocity;
    public Text YVelocity;
    public Text Perdu;
    public GameObject restart, backmain;
    public Sprite newSprite;
    public AudioSource explosion, reacteur;

    public LogicScriptBezier logic;

    Animator m_Animator;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScriptBezier>(); 
        m_Animator = gameObject.GetComponent<Animator>();
        Perdu.enabled = false;
        myRigidBody.transform.Rotate(new Vector3(0, 0, 0.001f));
    }

    void Update()
    {
        if(this!=null)
        {
            Perdu.enabled = false;
        }
        restart.SetActive(false);
        backmain.SetActive(false);

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

                if (PlayerPrefs.GetInt("Level") == 1)
                {
                    myRigidBody.AddForce(new Vector2(15f * -pushX, 45f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 2)
                {
                    myRigidBody.AddForce(new Vector2(45f * -pushX, 75f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 3)
                {
                    myRigidBody.AddForce(new Vector2(75f * -pushX, 105f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 4)
                {
                    myRigidBody.AddForce(new Vector2(105f * -pushX, 135 * pushY));
                }
            }
            if (myRigidBody.transform.rotation.z > -0.72 && myRigidBody.transform.rotation.z < 0)
            {
                pushX = 0.2f * Mathf.Sin(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);
                pushY = 0.2f * Mathf.Cos(((myRigidBody.transform.rotation.z * 90) / 0.72f) * Mathf.Deg2Rad);

                if (PlayerPrefs.GetInt("Level") == 1)
                {
                    myRigidBody.AddForce(new Vector2(15f * -pushX, 45f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 2)
                {
                    myRigidBody.AddForce(new Vector2(45f * -pushX, 75f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 3)
                {
                    myRigidBody.AddForce(new Vector2(75f * -pushX, 105f * pushY));
                }
                else if (PlayerPrefs.GetInt("Level") == 4)
                {
                    myRigidBody.AddForce(new Vector2(105f * -pushX, 135 * pushY));
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("NFeu");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Animator.SetTrigger("Feu");
            reacteur.Play();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (myRigidBody.transform.rotation.z < 0.715)
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
            restart.SetActive(true);
            backmain.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void bords()
    {

        if (gameObject.transform.position.x >= 10)
        {
            gameObject.transform.position = new Vector2(-10f, transform.position.y);
        }
        else if (gameObject.transform.position.x <= -10)
        {
            gameObject.transform.position = new Vector2(10f, transform.position.y);
        }
        else if (gameObject.transform.position.y >= 5.5f)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 5.5f);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "bezier" || c.gameObject.name == "Tourelle Bézier" || c.gameObject.name == "LaserBezier(Clone)")
        {
            Destroy(myRigidBody);
            spriteRenderer.sprite = newSprite;
            gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            m_Animator.SetTrigger("Explosion");
            explosion.Play();
            yield return new WaitForSeconds(1);
            altitude.text = "0.00";
            XVelocity.text = "0.000";
            YVelocity.text = "0.000";
            Perdu.enabled = true;
            restart.SetActive(true);
            backmain.SetActive(true);
            Destroy(gameObject);

            logic.timerIsRunning = false;
            logic.gameActive = false;
        }
    }
}
